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

        /// <summary>
        /// Creates a new listener for a button type.
        /// </summary>
        /// <param name="buttonName">Optional. The name of the button being pressed.</param>
        /// <param name="message">Optional. A specific message when the button is pressed.</param>
        public void NewButtonListener(string buttonName = "BUTTON", string message = "A button was pressed.") {
            // This code runs when a button is pressed

            Tracking tracking = new Tracking();
            tracking.ButtonListener(Path, buttonName, message, ProgramName);
        }

    }
}
