using System;
using System.Collections.Generic;
using System.Text;

namespace EzLyticsSDK {
    public class EzLytics {
        /// <summary>
        /// The path to the EzLytics file.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// A human friendly name for the program (optional).
        /// </summary>
        public string ProgramName { get; set; }

        /// <summary>
        /// Creates a new EzLytics file and begins analyzing.
        /// </summary>
        public void StartTracking() {
            Tracking tracking = new Tracking();

            Path = tracking.GenerateRandomFile();

            if (Path != "") {
                tracking.StartRecording(Path, ProgramName);
            }
        }

    }
}
