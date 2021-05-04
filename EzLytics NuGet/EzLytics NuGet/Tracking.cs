using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Security;
using System.Text;
using System.Threading;

namespace EzLyticsSDK {
    class Tracking {
        readonly string TEMP_FOLDER = Path.Combine(Path.GetTempPath() + "EzLytics\\");

        /// <summary>
        /// Creates a new EzLytics file.
        /// </summary>
        /// 
        /// <exception cref="PathTooLongException">The temporary folder path is too long.</exception>
        /// <exception cref="DirectoryNotFoundException">Part of the path was invalid.</exception>
        /// <exception cref="IOException">Failed to access the EzLytics temp folder.</exception>
        /// <exception cref="UnauthorizedAccessException">The function does not have authorization to create a folder there.</exception>
        /// 
        /// <returns>File path of the new EzLytics file.</returns>
        internal string GenerateRandomFile() {
            try {
                if (!Directory.Exists(TEMP_FOLDER)) {
                    Directory.CreateDirectory(TEMP_FOLDER);
                }
            }
            catch (PathTooLongException e) {
                // This should NEVER happen
                throw new PathTooLongException("The temporary folder path is too long. (Error 206)", e);
            }
            catch (DirectoryNotFoundException e) {
                // Might occur if the AppData file is missing. Might want to fix that.
                // TODO: Create a fix for this
                throw new DirectoryNotFoundException("Part of the path is invalid. (Error 205)", e);
            }
            catch (IOException e) {
                throw new IOException("Failed to create the EzLytics temp folder. (Error 203)", e);
            }


            string randomFile = string.Format(@"{0}.ezl", Guid.NewGuid());
            string filePath = Path.Combine(TEMP_FOLDER, randomFile);

            return filePath;
        }

        // TODO: We need to create a folder for all of the EzLytics files because how can we find it again if the program crashes? Also delete them when we send data?

        /// <summary>
        /// Records a basic activity.
        /// </summary>
	    /// 
        /// <param name="path">The path of the EzLytics file to update.</param>
        /// <param name="flag">The data flag.</param>
        /// <param name="recordType">The type of data recording.</param>
        /// <param name="message">The human-friendly message to go alongside the data.</param>
        /// <param name="programName">(Optional) The human-friendly program name.</param>
        /// 
        /// <example>BasicListener("C:\\Users\Public\\Downloads\\File.ezl", "button_press", "Button1", "Button 1 was pressed", "Example Program")</example>
        /// 
        /// <exception cref="IOException">The function could not access the EzLytics file.</exception>
        internal void BasicListener(string path, string flag, string recordType, string message, string programName) {
            // Get the start date of the listener
            string date = DateTime.Now.ToString();

            // Fill in the program name if it's not present
            if (programName == null || programName == "") {
                programName = AppDomain.CurrentDomain.FriendlyName;
            }

            // Format the arguments we were passed into one string
            Formatting formatting = new Formatting();
            string formatted = formatting.FormatNewLine(recordType, flag, message, date, programName);

            // Append to the data file
            File.AppendAllText(path, formatted + "\n");
        }
    }
}
