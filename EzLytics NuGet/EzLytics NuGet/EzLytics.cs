using System;
using System.IO;
using System.Collections.Generic;
using System.Net.Http;
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
	    /// A list of flags that the SDK already uses.
        /// </summary>
        readonly private string[] unuseableFlags = {
            "program_start"
        };

        /// <summary>
        /// A list of listeners that the SDK already uses.
        /// </summary>
        readonly private string[] unuseableListeners = {
            "Auto"
        };

	    private int privateDataSendInterval = 60;
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

        /// <summary>
        /// Creates a new EzLytics file and begins analyzing.
        /// </summary>
        /// 
        /// <exception cref="PathTooLongException">The path to the temporary folder was too long.</exception>
        /// <exception cref="DirectoryNotFoundException">Part of the path was invalid. Perhaps the user deleted the AppData folder?</exception>
        /// <exception cref="IOException">Failed to access the EzLytics temporary folder.</exception>
        /// <exception cref="UnauthorizedAccessException">The function does not have access to create a folder in AppData.</exception>
        public void StartTracking() {
            Tracking tracking = new Tracking();

            Path = tracking.GenerateRandomFile();

            if (Path != "") {
                tracking.BasicListener(Path, "program_start", "Auto", "The program has started.", ProgramName);
            }
        }

        /// <summary>
        /// Creates a new basic listener for any type.
        /// </summary>
        /// 
        /// <param name="flag">The desired type flag.</param>
        /// <param name="listenerName">The specific name of the listener.</param>
        /// <param name="message">The human-friendly message to go alongside the data.</param>
        ///
        /// <exception cref="IOException">The function could not access the EzLytics data file.</exception>
        
        // TODO: Make sure the flag isn't program_start or anything bad. Also make sure the name isn't Auto.
        public void NewBasicListener(string flag = "basic_listener", 
            string listenerName = "New listener", 
            string message = "A basic listener was activated.") {
            // Check for unuseable system data
            CheckData(listenerName, flag);

            Tracking tracking = new Tracking();
            tracking.BasicListener(Path, flag, listenerName, message, ProgramName);
        }

        #region Various pre-made listeners
        /// <summary>
        /// Creates a new listener for a button type.
        /// </summary>
        /// 
        /// <param name="buttonName">The name of the button to listen to.</param>
        /// <param name="message">The human-friendly message to go alongside the data.</param>
        /// 
        /// <exception cref="IOException">The function could not access the EzLytics data file.</exception>
        /// <exception cref="EzLyticsException">The data set was invalid.</exception>
        public void NewButtonListener(string buttonName = "Button", string message = "A button was pressed.") {
            // Check for unuseable system data
            CheckData(buttonName);

            Tracking tracking = new Tracking();
            tracking.BasicListener(Path, "button_press", buttonName, message, ProgramName);
        }

        /// <summary>
        /// Creates a new mouse listener.
        /// </summary>
        /// 
        /// <param name="mouseButton">(Optional) Which mouse button was pressed.</param>
        /// <param name="message">The human-friendly message to go alongside the data.</param>
        /// 
        /// <exception cref="IOException">The function could not access the EzLytics data file.</exception>
        /// <exception cref="EzLyticsException">The data set was invalid.</exception>
        public void NewMouseListener(string mouseButton = "Mouse", string message = "The mouse was clicked.") {
            // Check for unuseable system data
            CheckData(mouseButton);

            Tracking tracking = new Tracking();
            tracking.BasicListener(Path, "mouse_press", mouseButton, message, ProgramName);
        }

        /// <summary>
        /// Creates a new keyboard listener.
        /// </summary>
        /// 
        /// <param name="mouseButton">(Optional) Which mouse button was pressed.</param>
        /// <param name="message">The human-friendly message to go alongside the data.</param>
        /// 
        /// <exception cref="IOException">The function could not access the EzLytics data file.</exception>
        /// <exception cref="EzLyticsException">The data set was invalid.</exception>
        public void NewKeyboardListener(string keyPressed = "Key", string message = "A key was pressed.") {
            // Check for unuseable system data
            CheckData(keyPressed);

            Tracking tracking = new Tracking();
            tracking.BasicListener(Path, "key_press", keyPressed, message, ProgramName);
        }
        #endregion

        /// <summary>
        /// Checks data against an invalid data set.
        /// If the data is invalid, it throws an EzLyticsException with a variable message.
        /// 
        /// This is done so that the user doesn't accidentally 
        /// use a system flag\listener, like 'program_start' and
        /// cause the program to trip and think it just started again.
        /// </summary>
        /// 
        /// <param name="data">The data to check.</param>
        /// <param name="unuseableData">The list of invalid data.</param>
        /// <param name="exceptionMessage">The exception message if the data is invalid.</param>
        /// 
        /// <exception cref="EzLyticsException">The data set was invalid.</exception>
        private void CheckData(string listener = "", string flag = "") {
            foreach (string i in unuseableListeners) {
                if (listener == i) {
                    throw new ArgumentException(listener + " is an invalid listener name! (Error 301)");
                }
            }

            foreach (string i in unuseableFlags) {
                if (flag == i) {
                    throw new ArgumentException(flag + " is an invalid flag! (Error 302)");
                }
            }
        }
    }
}
