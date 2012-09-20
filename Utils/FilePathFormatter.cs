using System;
using System.IO;

namespace Swensen.Utils {
    public enum FilePathFormat {
        Full, Short, Ellipsis, ShortFileFullDir, FullDir
    }

    public static class FilePathFormatter {
        public static string Format(this FileInfo filePath, FilePathFormat format) {
            switch (format) {
                case FilePathFormat.Full:
                    return filePath.FullName;
                case FilePathFormat.Short:
                    return filePath.Name;
                case FilePathFormat.ShortFileFullDir:
                    return string.Format("{0} ({1})", filePath.Name, filePath.DirectoryName);
                case FilePathFormat.Ellipsis:
                    return string.Format(@"{1}...{0}{2}{0}{3}", Path.DirectorySeparatorChar, filePath.Directory.Root.Name, filePath.Directory.Name, filePath.Name);
                case FilePathFormat.FullDir:
                    return filePath.DirectoryName;
                default:
                    throw new ArgumentOutOfRangeException("format");
            }
        }

        public static string Format(string filePath, FilePathFormat format) {
            if (filePath.IsBlank())
                return "";

            return Format(new FileInfo(filePath), format);
        }
    }
}