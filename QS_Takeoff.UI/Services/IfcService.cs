using System;

namespace QS_Takeoff.UI.Services {
    public class IfcService {
        public void LoadIfc(string path) {
            // Placeholder for IFC parsing logic
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException("Path cannot be empty", nameof(path));
        }
    }
}
