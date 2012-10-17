using System;
using System.Collections.Generic;

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
    }
}
