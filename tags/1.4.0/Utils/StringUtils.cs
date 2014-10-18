using System;

namespace Swensen.Utils {
    public enum StringCoalesceMode { Null, NullOrEmpty, NullOrBlank }

    public static class StringUtils {
        /// <summary>
        /// return String.IsNullOrWhiteSpace(input);
        /// </summary>
        /// <param name="?"></param>
        /// <param name="input"> </param>
        /// <returns></returns>
        public static bool IsBlank(this string input) {
            return String.IsNullOrWhiteSpace(input);
        }

        /// <summary>
        /// Coalesce the given string with the given mode using the given fallback.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="fallback"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public static string Coalesce(this string input, string fallback, StringCoalesceMode mode) {
            switch (mode) {
                case StringCoalesceMode.Null :
                    return input ?? fallback;
                case StringCoalesceMode.NullOrEmpty:
                    return string.IsNullOrEmpty(input) ? fallback : input;
                case StringCoalesceMode.NullOrBlank:
                    return string.IsNullOrWhiteSpace(input) ? fallback : input;
                default:
                    throw new ArgumentOutOfRangeException("mode");
            }
        }

        /// <summary>
        /// Coalesce the given string using the NullOrBlank mode with the given fallback.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="fallback"></param>
        /// <returns></returns>
        public static string Coalesce(this string input, string fallback) {
            return Coalesce(input, fallback, StringCoalesceMode.NullOrBlank);
        }

        /// <summary>
        /// Coalesce the given string using the NullOrBlank mode with "" as the fallback.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Coalesce(this string input) {
            return Coalesce(input, "", StringCoalesceMode.NullOrBlank);
        }
    }
}
