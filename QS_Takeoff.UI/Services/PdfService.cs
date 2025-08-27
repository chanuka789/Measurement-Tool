using System;

namespace QS_Takeoff.UI.Services {
    public class PdfService {
        public void LoadPdf(string path) {
            // Placeholder for PDF parsing logic
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException("Path cannot be empty", nameof(path));
        }
    }
}
