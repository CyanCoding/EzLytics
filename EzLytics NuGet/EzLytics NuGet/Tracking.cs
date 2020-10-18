using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace EzLyticsSDK {
    class Tracking {
        public string Start() {
            string randomFile = string.Format(@"{0}.ezl", Guid.NewGuid());

            string filePath = Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.ApplicationData), randomFile);

            return filePath;
        }
    }
}
