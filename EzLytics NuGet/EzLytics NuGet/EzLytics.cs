using System;
using System.Collections.Generic;
using System.Text;

namespace EzLyticsSDK {
    public class EzLytics {
        public string path { get; set; }

        public void StartTracking() {
            Tracking tracking = new Tracking();

            path = tracking.Start();
        }

    }
}
