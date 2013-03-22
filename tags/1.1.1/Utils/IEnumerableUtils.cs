using System;
using System.Collections.Generic;
using System.Linq;

namespace Swensen.Utils {
    public static class IEnumerableUtils {
        public static IEnumerable<T> Coalesce<T>(this IEnumerable<T> input, IEnumerable<T> fallback) {
            return input ?? fallback;
        }

        public static IEnumerable<T> Coalesce<T>(this IEnumerable<T> input) {
            return input.Coalesce(new T[] { });
        }

        public static string Join(this IEnumerable<string> input, string sep) {
            return String.Join(sep, input);
        }

        /// <summary>
        /// Force the enumeration of this sequence, returning a readonly wrapper for the results.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        public static IEnumerable<T> Force<T>(this IEnumerable<T> input) {
            return input.ToList().AsReadOnly().AsEnumerable();
        }
    }
}
