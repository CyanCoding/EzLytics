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
        /// <exception cref="IOException">Failed to access the EzLytics temp folder.</exception>
        /// 
        /// <returns>File path of the new EzLytics file.</returns>
        public string GenerateRandomFile() {
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
            catch (UnauthorizedAccessException e) {
                throw new UnauthorizedAccessException("You are not authorized to create a folder at " + TEMP_FOLDER + ". (Error 204)", e);
            }


            string randomFile = string.Format(@"{0}.ezl", Guid.NewGuid());
            string filePath = Path.Combine(TEMP_FOLDER, randomFile);

            return filePath;
        }

        // TODO: We need to create a folder for all of the EzLytics files because how can we find it again if the program crashes? Also delete them when we send data?

        /// <summary>
        /// Starts the program analysis.
        /// </summary>
        /// <param name="path">The path to the EzLytics file.</param>
        /// <param name="programName">A human friendly program name. (optional)</param>
        public void StartRecording(string path, string programName) {
            string recordType = "AUTO";
            string flag = "program_start";
            string message = "The program has started.";
            string date = DateTime.Now.ToString();

            if (programName == null || programName == "") {
                programName = AppDomain.CurrentDomain.FriendlyName;
            }

            Formatting formatting = new Formatting();
            string formatted = formatting.FormatNewLine(recordType, flag, message, date, programName);

            IO fileIO = new IO();

            try {
                fileIO.WriteToGeneralFile(path, formatted);
            }
            catch (IOException e) {
                throw new IOException("Unable to access " + path + " EzLytics file. (Error 201)", e);
            }
            
        }

        internal void ButtonListener(string path, string buttonName, string message, string programName) {
            string recordType = buttonName;
            string flag = "button_press";
            string date = DateTime.Now.ToString();
            // By default the message is "A button was pressed."

            if (programName == null || programName == "") {
                programName = AppDomain.CurrentDomain.FriendlyName;
            }

            Formatting formatting = new Formatting();
            string formatted = formatting.FormatNewLine(recordType, flag, message, date, programName);

            IO fileIO = new IO();

            try {
                fileIO.WriteToGeneralFile(path, formatted);
            }
<<<<<<< Updated upstream
=======
            catch (IOException e) {
                throw new IOException("Unable to access " + path + " EzLytics file. (Error 202)", e);
            }
>>>>>>> Stashed changes
        }
    }
}
