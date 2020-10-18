using System;
using System.Collections.Generic;
using System.Text;

namespace EzLyticsSDK {
    public class EzLytics {
        /// <summary>
        /// The path to the EzLytics file.
        /// </summary>
        public string path { get; set; }

        /// <summary>
        /// Creates a new EzLytics file and begins analyzing.
        /// </summary>
        public void StartTracking() {
            Tracking tracking = new Tracking();

            path = tracking.Start();

            if (path != "") {
                tracking.StartRecording(path);
            }
        }

    }
}
