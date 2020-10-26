using System;
using System.Collections.Generic;
using System.Text;

namespace EzLyticsSDK {
    public class EzLytics {
        /// <summary>
        /// The path to the EzLytics file.
        /// </summary>
        private string Path { get; set; }

        /// <summary>
        /// A human friendly name for the program (optional).
        /// </summary>
        public string ProgramName { get; set; }

        /// <summary>
        /// How often in seconds to send data to the server.
        /// By default it is set to 60 seconds.
        /// Must be in a range of 60 - 300 seconds.
        /// </summary>
        public int DataSendInterval {
            get { return privateDataSendInterval; }
            set {
                if (value < 60) {
                    value = 60;
                }
                else if (value > 300) {
                    value = 300;
                }

                privateDataSendInterval = value;
            }
        }
        private int privateDataSendInterval = 60;

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
