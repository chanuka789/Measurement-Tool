using System;
using System.IO;

namespace QS_Takeoff.UI.Services {
    public class FileService {
        private readonly PdfService _pdfService = new();
        private readonly IfcService _ifcService = new();
        private readonly ObjService _objService = new();
        private readonly DwfxService _dwfxService = new();

        public void Open(string path) {
            var ext = Path.GetExtension(path)?.ToLowerInvariant();
            switch (ext) {
                case ".pdf":
                    _pdfService.LoadPdf(path);
                    break;
                case ".ifc":
                    _ifcService.LoadIfc(path);
                    break;
                case ".obj":
                    _objService.LoadObj(path);
                    break;
                case ".dwfx":
                    _dwfxService.LoadDwfx(path);
                    break;
                default:
                    throw new NotSupportedException($"Unsupported file type: {ext}");
            }
        }
    }
}
