using System;
using System.Collections.Generic;
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
        /// <returns>File path of the new EzLytics file.</returns>
        public string GenerateRandomFile() {
            try {
                if (!Directory.Exists(TEMP_FOLDER)) {
                    Directory.CreateDirectory(TEMP_FOLDER);
                }
            }
            catch (Exception) {
                // Could not write to that folder
                throw new Exception("Failed to access the EzLytics temp folder. (Error 200)");
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

            // Example output:
            // ["AUTO", "program_start", "The program has started.", "10/18/2020 00:00:00", "Goal Getter"]
            string formatted = string.Format(
                "[\"{0}\", \"{1}\", \"{2}\", \"{3}\", \"{4}\"]",
                recordType, flag, message, date, programName);

            try {
                FileStream fileStream = File.Open(path, FileMode.OpenOrCreate, FileAccess.Write);

                StreamWriter streamWriter = new StreamWriter(fileStream);

                streamWriter.WriteLine(formatted);
                streamWriter.Flush();
                streamWriter.Close();
            }
            catch (IOException) {
                throw new Exception("Unable to access" + path + " EzLytics file. (Error 201)");
            }
        }
    }
}
