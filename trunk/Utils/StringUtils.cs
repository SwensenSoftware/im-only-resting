using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Swensen.Utils {
    public static class StringUtils {
        /// <summary>
        /// return String.IsNullOrWhiteSpace(input);
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public static bool IsBlank(this string input) {
            return String.IsNullOrWhiteSpace(input);
        }
    }
}
