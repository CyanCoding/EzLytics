using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EzLyticsSDK {
    class IO {
        /// <summary>
        /// Attempts to write data to a file.
        /// Can throw an IO, Path Not Found, Security, or other exception.
        /// </summary>
        /// 
        /// <param name="path">The path to the file to write to.</param>
        /// <param name="data">The data to write to the file.</param>
        /// 
        /// <exception cref="IOException">The function could not write to the file.</exception>
        /// <exception cref="ArgumentNullException">Invalid arguments.</exception>
        /// <exception cref="PathTooLongException">The path is too long.</exception>
        /// <exception cref="UnauthorizedAccessException">The function does not have access to the file.</exception>
        /// <exception cref="NotSupportedException">The stream does not support writing to the file.</exception>
        internal void WriteToGeneralFile(string path, string data) {
            FileStream fileStream = File.Open(path, FileMode.OpenOrCreate, FileAccess.Write);

            StreamWriter streamWriter = new StreamWriter(fileStream);

            streamWriter.WriteLine(data);
            streamWriter.Flush();
            streamWriter.Close();
        }
    }
}
