using System;
using System.Collections.Generic;
using System.Linq;

namespace Decision.Framework.LinqToolkit
{
    public static class EnumerableExtensions
    {
        public static List<T> RemoveDuplicates<T>(IEnumerable<T> items, IEqualityComparer<T> comparer)
        {
            return (from s in items select s).Distinct(comparer).ToList();
        }
        public static List<T> RemoveDuplicates<T>(IEnumerable<T> items)
        {
            return (from s in items select s).Distinct().ToList();
        }
        public static string Uniquify(this IEnumerable<string> inputStrings, string targetString)
        {
            var num = 0;
            while (inputStrings.Any<string>((Func<string, bool>)(n => string.Equals(n, targetString, StringComparison.Ordinal))))
                targetString += (string)(object)++num;
            return targetString;
        }

        public static void Each<T>(this IEnumerable<T> ts, Action<T, int> action)
        {
            var num = 0;
            foreach (var t in ts)
                action?.Invoke(t, num++);
        }

        public static void Each<T>(this IEnumerable<T> ts, Action<T> action)
        {
            foreach (var t in ts)
                action(t);
        }

        public static void Each<T, S>(this IEnumerable<T> ts, Func<T, S> action)
        {
            foreach (var t in ts)
            {
                var s = action(t);
            }
        }

        public static string Join<T>(this IEnumerable<T> ts, Func<T, string> selector = null, string separator = ", ")
        {
            selector = selector ?? (Func<T, string>)(t => t.ToString());
            return string.Join(separator, ts.Where<T>((Func<T, bool>)(t => !object.ReferenceEquals((object)t, (object)null))).Select<T, string>(selector));
        }

        public static IEnumerable<TSource> Prepend<TSource>(this IEnumerable<TSource> source, TSource value)
        {
            yield return value;
            foreach (var source1 in source)
                yield return source1;
        }

        public static IEnumerable<TSource> Append<TSource>(this IEnumerable<TSource> source, TSource value)
        {
            foreach (var source1 in source)
                yield return source1;
            yield return value;
        }

    }
}
