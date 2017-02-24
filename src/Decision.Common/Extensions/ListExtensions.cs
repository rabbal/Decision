using System;
using System.Collections.Generic;
using System.Text;
using NTierMvcFramework.Common.Infrastructure;

namespace NTierMvcFramework.Common.Extensions
{
    public static class ListExtensions
    {
        public static string ToSeparatedString<T>(this IList<T> value)
        {
            return ToSeparatedString(value, ",");
        }

        public static string ToConcatenatedString(
           this IList<string> list
           , string separator)
        {
            return string.Join(separator, list);
        }

        public static string ToConcatenatedString(
            this string[] list
            , string separator)
        {
            return string.Join(separator, list);
        }
        public static string ToSeparatedString<T>(this IList<T> value, string separator)
        {
            if (value.Count == 0)
            {
                return string.Empty;
            }
            if (value.Count == 1)
            {
                if (value[0] != null)
                {
                    return value[0].ToString();
                }
                return string.Empty;
            }

            var builder = new StringBuilder();
            var flag = true;
            var flag2 = false;
            foreach (object obj2 in value)
            {
                if (!flag)
                {
                    builder.Append(separator);
                }
                if (obj2 != null)
                {
                    builder.Append(obj2.ToString().TrimEnd());
                    flag2 = true;
                }
                flag = false;
            }
            if (!flag2)
            {
                return string.Empty;
            }
            return builder.ToString();
        }

        /// <summary>
        ///     Makes a slice of the specified list in between the start and end indexes.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="start">The start index.</param>
        /// <param name="end">The end index.</param>
        /// <returns>A slice of the list.</returns>
        public static IList<T> Slice<T>(this IList<T> list, int? start, int? end)
        {
            return list.Slice(start, end, null);
        }

        /// <summary>
        ///     Makes a slice of the specified list in between the start and end indexes,
        ///     getting every so many items based upon the step.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="start">The start index.</param>
        /// <param name="end">The end index.</param>
        /// <param name="step">The step.</param>
        /// <returns>A slice of the list.</returns>
        public static IList<T> Slice<T>(this IList<T> list, int? start, int? end, int? step)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            if (step == 0)
                throw Error.Argument(nameof(step), "Step cannot be zero.");

            var slicedList = new List<T>();

            // nothing to slice
            if (list.Count == 0)
                return slicedList;

            // set defaults for null arguments
            var s = step ?? 1;
            var startIndex = start ?? 0;
            var endIndex = end ?? list.Count;

            // start from the end of the list if start is negative
            startIndex = startIndex < 0 ? list.Count + startIndex : startIndex;

            // end from the start of the list if end is negative
            endIndex = endIndex < 0 ? list.Count + endIndex : endIndex;

            // ensure indexes keep within collection bounds
            startIndex = Math.Max(startIndex, 0);
            endIndex = Math.Min(endIndex, list.Count - 1);

            // loop between start and end indexes, incrementing by the step
            for (var i = startIndex; i < endIndex; i += s)
            {
                slicedList.Add(list[i]);
            }

            return slicedList;
        }
    }
}