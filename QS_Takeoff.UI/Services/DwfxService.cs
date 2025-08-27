using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Numerics;
using System.Xml.Linq;

namespace QS_Takeoff.UI.Services
{
    /// <summary>
    /// Service for reading basic information from DWFx files.
    /// The implementation focuses on discovering sections and extracting
    /// primitive geometry data as meshes and layers.  It is intentionally
    /// lightweight and only implements a subset of the DWFx specification
    /// sufficient for measurement tooling.
    /// </summary>
    public class DwfxService
    {
        private readonly List<DwfxDrawing> _drawings = new();
        private readonly List<DwfxModel> _models = new();

        /// <summary>
        /// Loads a DWFx package from disk and populates 2D/3D structures
        /// within this service.  The method inspects <c>_manifest.xml</c>
        /// in the root of the package to discover <c>EPlotSection</c> and
        /// <c>EModelSection</c> entries and then reads the referenced parts.
        /// </summary>
        /// <param name="path">Path to the DWFx file on disk.</param>
        public void LoadDwfx(string path)
        {
            _drawings.Clear();
            _models.Clear();

            using var package = Package.Open(path, FileMode.Open, FileAccess.Read);

            var manifestUri = new Uri("/_manifest.xml", UriKind.Relative);
            if (!package.PartExists(manifestUri))
                throw new FileNotFoundException("DWFx manifest not found.", path);

            XDocument manifest;
            using (var stream = package.GetPart(manifestUri).GetStream())
            {
                manifest = XDocument.Load(stream);
            }

            // Namespace is optional in practice, so we search by local name.
            foreach (var section in manifest.Descendants().Where(e => e.Name.LocalName == "EPlotSection"))
            {
                var resource = section.Descendants().FirstOrDefault(e => e.Name.LocalName == "Resource");
                var href = resource?.Attribute("HRef")?.Value;
                if (href == null)
                    continue;

                var drawing = ParseDrawing(package, new Uri(href, UriKind.Relative));
                drawing.Name = section.Attribute("Name")?.Value;
                drawing.UnitScale = GetUnitScale(section.Attribute("Units")?.Value);
                _drawings.Add(drawing);
            }

            foreach (var section in manifest.Descendants().Where(e => e.Name.LocalName == "EModelSection"))
            {
                var resource = section.Descendants().FirstOrDefault(e => e.Name.LocalName == "Resource");
                var href = resource?.Attribute("HRef")?.Value;
                if (href == null)
                    continue;

                var model = ParseModel(package, new Uri(href, UriKind.Relative));
                model.Name = section.Attribute("Name")?.Value;
                model.UnitScale = GetUnitScale(section.Attribute("Units")?.Value);
                model.Transform = ParseTransform(section.Attribute("Transform")?.Value);
                _models.Add(model);
            }
        }

        /// <summary>
        /// Returns the list of 2D drawing sections parsed from the last loaded
        /// DWFx file.
        /// </summary>
        public IReadOnlyList<DwfxDrawing> Get2DDrawings() => _drawings;

        /// <summary>
        /// Returns the list of 3D model sections parsed from the last loaded
        /// DWFx file.
        /// </summary>
        public IReadOnlyList<DwfxModel> Get3DModels() => _models;

        private static DwfxDrawing ParseDrawing(Package package, Uri partUri)
        {
            var drawing = new DwfxDrawing();
            var part = package.GetPart(partUri);

            using var stream = part.GetStream();
            using var subPackage = Package.Open(stream);

            foreach (var p in subPackage.GetParts())
            {
                var path = p.Uri.ToString();
                if (path.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
                {
                    using var s = p.GetStream();
                    var doc = XDocument.Load(s);
                    foreach (var layerElem in doc.Descendants().Where(e => e.Name.LocalName == "Layer"))
                    {
                        var layer = new DwfxLayer { Name = layerElem.Attribute("Name")?.Value ?? string.Empty };
                        drawing.Layers.Add(layer);
                    }
                }
                else if (path.EndsWith(".w2d", StringComparison.OrdinalIgnoreCase))
                {
                    using var s = p.GetStream();
                    var layer = ReadLayerGeometry(s);
                    drawing.Layers.Add(layer);
                }
            }

            return drawing;
        }

        private static DwfxModel ParseModel(Package package, Uri partUri)
        {
            var model = new DwfxModel();
            var part = package.GetPart(partUri);

            using var stream = part.GetStream();
            using var subPackage = Package.Open(stream);

            foreach (var p in subPackage.GetParts())
            {
                var path = p.Uri.ToString();
                if (path.EndsWith(".w3d", StringComparison.OrdinalIgnoreCase))
                {
                    using var s = p.GetStream();
                    var mesh = ReadMeshGeometry(s);
                    model.Meshes.Add(mesh);
                }
                else if (path.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
                {
                    using var s = p.GetStream();
                    var doc = XDocument.Load(s);
                    ParseModelMetadata(doc, model);
                }
            }

            return model;
        }

        private static void ParseModelMetadata(XDocument doc, DwfxModel model)
        {
            foreach (var layer in doc.Descendants().Where(e => e.Name.LocalName == "Layer"))
            {
                var name = layer.Attribute("Name")?.Value;
                if (!string.IsNullOrEmpty(name))
                    model.Layers.Add(name);
            }
        }

        private static DwfxLayer ReadLayerGeometry(Stream stream)
        {
            using var ms = new MemoryStream();
            stream.CopyTo(ms);
            return new DwfxLayer
            {
                Name = "Geometry",
                Geometry = ms.ToArray()
            };
        }

        private static DwfxMesh ReadMeshGeometry(Stream stream)
        {
            var mesh = new DwfxMesh();
            using var reader = new BinaryReader(stream);
            try
            {
                // Very small subset of possible geometry layout: the stream
                // starts with vertex count, followed by that many XYZ triples,
                // then index count and the triangle indices.  This matches the
                // layout produced by some W3D writers and is sufficient for
                // basic preview/measurement scenarios.
                int vertexCount = reader.ReadInt32();
                for (int i = 0; i < vertexCount; i++)
                {
                    mesh.Vertices.Add(new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle()));
                }

                int indexCount = reader.ReadInt32();
                for (int i = 0; i < indexCount; i++)
                {
                    mesh.Indices.Add(reader.ReadInt32());
                }
            }
            catch (EndOfStreamException)
            {
                // If the stream is shorter than expected we simply return the
                // geometry that could be read.
            }

            return mesh;
        }

        private static double GetUnitScale(string? unit)
        {
            return unit?.ToLowerInvariant() switch
            {
                "mm" or "millimeter" or "millimetre" => 0.001,
                "cm" or "centimeter" or "centimetre" => 0.01,
                "m" or "meter" or "metre" => 1.0,
                "in" or "inch" => 0.0254,
                "ft" or "foot" => 0.3048,
                _ => 1.0
            };
        }

        private static Matrix4x4 ParseTransform(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return Matrix4x4.Identity;

            var numbers = value.Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)
                               .Select(v => float.Parse(v, System.Globalization.CultureInfo.InvariantCulture))
                               .ToArray();

            if (numbers.Length == 16)
            {
                return new Matrix4x4(
                    numbers[0], numbers[1], numbers[2], numbers[3],
                    numbers[4], numbers[5], numbers[6], numbers[7],
                    numbers[8], numbers[9], numbers[10], numbers[11],
                    numbers[12], numbers[13], numbers[14], numbers[15]);
            }

            return Matrix4x4.Identity;
        }

        #region Data classes

        public class DwfxDrawing
        {
            public string? Name { get; set; }
            public double UnitScale { get; set; } = 1.0;
            public List<DwfxLayer> Layers { get; } = new();
        }

        public class DwfxLayer
        {
            public string Name { get; set; } = string.Empty;
            public byte[] Geometry { get; set; } = Array.Empty<byte>();
            public List<string> ObjectIds { get; } = new();
        }

        public class DwfxModel
        {
            public string? Name { get; set; }
            public double UnitScale { get; set; } = 1.0;
            public Matrix4x4 Transform { get; set; } = Matrix4x4.Identity;
            public List<string> Layers { get; } = new();
            public List<DwfxMesh> Meshes { get; } = new();
        }

        public class DwfxMesh
        {
            public string Layer { get; set; } = string.Empty;
            public string ObjectId { get; set; } = string.Empty;
            public Matrix4x4 Transform { get; set; } = Matrix4x4.Identity;
            public List<Vector3> Vertices { get; } = new();
            public List<int> Indices { get; } = new();
            public Dictionary<string, string> Metadata { get; } = new();
        }

        #endregion
    }
}

