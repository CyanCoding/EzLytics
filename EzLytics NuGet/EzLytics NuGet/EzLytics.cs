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
        public string Path { get; set; }

        /// <summary>
        /// A human friendly name for the program (optional).
        /// </summary>
        public string ProgramName { get; set; }

        /// <summary>
        /// A list of flags that the SDK already uses.
        /// </summary>
        private string[] unuseableFlags = {
            "program_start"
        };

        /// <summary>
        /// A list of listeners that the SDK already uses.
        /// </summary>
        private string[] unuseableListeners = {
            "Auto"
        };

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
        public void NewBasicListener(string flag = "basic_listener", string listenerName = "New listener", string message = "A basic listener was activated.") {
            // Check for unuseable system data
            CheckData(flag, unuseableFlags, flag + " is a system flag. Check the flag list in the EzLytics documentation for useable flags (error 208).");
            CheckData(listenerName, unuseableListeners, listenerName + " is a system listener name and cannot be used." +
                        "Check the listener list in the EzLytics documentation for useable listener names (error 209).");

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
            CheckData(buttonName, unuseableListeners, buttonName + " is a system listener name and cannot be used." +
                        "Check the listener list in the EzLytics documentation for useable listener names (error 209).");

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
            CheckData(mouseButton, unuseableListeners, mouseButton + " is a system listener name and cannot be used." +
                        "Check the listener list in the EzLytics documentation for useable listener names (error 209).");

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
            Tracking tracking = new Tracking();
            tracking.BasicListener(Path, "key_press", keyPressed, message, ProgramName);
        }
        #endregion

        /// <summary>
        /// Checks data against an invalid data set.
        /// If the data is invalid, it throws an EzLyticsException with a variable message.
        /// </summary>
        /// 
        /// <param name="data">The data to check.</param>
        /// <param name="unuseableData">The list of invalid data.</param>
        /// <param name="exceptionMessage">The exception message if the data is invalid.</param>
        /// 
        /// <exception cref="EzLyticsException">The data set was invalid.</exception>
        private void CheckData(string data, string[] unuseableData, string exceptionMessage) {
            foreach (string unuseableDataString in unuseableData) {
                if (unuseableDataString == data) {
                    throw new EzLyticsException(exceptionMessage);
                }
            }
        }
    }
}
