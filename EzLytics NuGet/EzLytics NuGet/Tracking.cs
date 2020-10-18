using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Text;
using System.Threading;

namespace EzLyticsSDK {
    class Tracking {
        /// <summary>
        /// Creates a new EzLytics file.
        /// </summary>
        /// <returns>File path of the new EzLytics file.</returns>
        public string Start() {
            string randomFile = string.Format(@"{0}.ezl", Guid.NewGuid());
            string filePath = "";

            try {
                filePath = Path.Combine(Path.GetTempPath(), randomFile);
            }
            catch (SecurityException) {
                throw new Exception("Could not access the EzLytics temp folder.");
            }

            return filePath;
        }

        /// <summary>
        /// Starts the program analysis.
        /// </summary>
        /// <param name="path">The path to the EzLytics file.</param>
        public void StartRecording(string path) {
            string recordType = "AUTO";
            string flag = "program_start";
            string message = "The program has started.";
            string date = DateTime.Now.ToString();
            string programName = System.AppDomain.CurrentDomain.FriendlyName;

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
                throw new Exception("Unable to access" + path + " EzLytics file.");
            }
        }
    }
}
