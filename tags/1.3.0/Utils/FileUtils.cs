using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Swensen.Utils {
    public static class FileUtils {
        public static string CreateTempFilePath(string extension) {
            var path = Path.GetTempPath();
            var fileName = Guid.NewGuid().ToString() + (extension.IsBlank() ? "" : "." + extension);
            var fullFileName = Path.Combine(path, fileName);
            return fullFileName;
        }

        /// <summary>
        /// Creates the temporary file with the given extension and returns its full path.
        /// </summary>
        /// <param name="contentBytes"></param>
        /// <param name="extension"> </param>
        /// <returns></returns>
        public static string CreateTempFile(byte[] contentBytes, string extension) {
            var fullFileName = CreateTempFilePath(extension);            
            File.WriteAllBytes(fullFileName, contentBytes);
            return fullFileName;
        }
    }
}
