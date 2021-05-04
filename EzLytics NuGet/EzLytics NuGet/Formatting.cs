using System;
using System.Collections.Generic;
using System.Text;

namespace EzLyticsSDK {
    class Formatting {
        /// <summary>
        /// Creates formatting for a basic new data line.
        /// </summary>
        /// 
        /// <param name="recordType">The type of media being recorded.</param>
        /// <param name="flag">The media flag.</param>
        /// <param name="message">The human-friendly message.</param>
        /// <param name="date">The current DateTime.</param>
        /// <param name="programName">The program name.</param>
        /// 
        /// <exception cref="FormatException">An error occurred while formatting.</exception>
        /// 
        /// <returns>A properly formatted string.</returns>
        internal string FormatNewLine(string recordType, string flag, string message, string date, string programName) {
            // Example output:
            // ["AUTO", "program_start", "The program has started.", "10/18/2020 00:00:00", "Goal Getter"]

            string formattedString = "";

            try {
                formattedString = string.Format(
                    "[\"{0}\", \"{1}\", \"{2}\", \"{3}\", \"{4}\"]",
                    recordType, flag, message, date, programName
                );
            }
            catch (FormatException e) {
                throw new FormatException("An error occurred while formatting. (Error 207)", e);
            }


            return formattedString;
        }
    }
}
