using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;

namespace QS_Takeoff.UI.Services
{
    /// <summary>
    /// Basic reader for DWFX packages. Extracts URIs for 2D and 3D parts
    /// so that callers can later parse geometry data.
    /// </summary>
    public class DwfxService
    {
        private readonly List<Uri> _drawingParts = new();
        private readonly List<Uri> _modelParts = new();

        public void LoadDwfx(string path)
        {
            _drawingParts.Clear();
            _modelParts.Clear();

            using var package = Package.Open(path, FileMode.Open, FileAccess.Read);
            foreach (var part in package.GetParts())
            {
                var uri = part.Uri;
                var id = uri.OriginalString;
                if (id.Contains("EPlotSection", StringComparison.OrdinalIgnoreCase))
                {
                    _drawingParts.Add(uri);
                }
                else if (id.Contains("EModelSection", StringComparison.OrdinalIgnoreCase))
                {
                    _modelParts.Add(uri);
                }
            }
        }

        public IEnumerable<Uri> Get2DDrawings() => _drawingParts;

        public IEnumerable<Uri> Get3DModels() => _modelParts;
    }
}
