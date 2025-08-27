using System;

namespace QS_Takeoff.UI.Services {
    public class ObjService {
        public void LoadObj(string path) {
            // Placeholder for OBJ parsing logic
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException("Path cannot be empty", nameof(path));
        }
    }
}
