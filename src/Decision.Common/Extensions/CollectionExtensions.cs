using System.Collections.Generic;
using System.Linq;

namespace NTierMvcFramework.Common.Extensions
{
    public static class CollectionExtensions
    {
        public static void AddRange<T>(this ICollection<T> initial, IEnumerable<T> other)
        {
            if (other == null)
                return;

            var list = initial as List<T>;

            if (list != null)
            {
                list.AddRange(other);
                return;
            }

            other.Each(initial.Add);
        }

        public static bool IsNullOrEmpty<T>(this ICollection<T> source)
        {
            return source == null || source.Count == 0;
        }


        public static bool EqualsAll<T>(this IList<T> a, IList<T> b)
        {
            if (a == null || b == null)
                return a == null && b == null;

            if (a.Count != b.Count)
                return false;

            var comparer = EqualityComparer<T>.Default;

            return !a.Where((t, i) => !comparer.Equals(t, b[i])).Any();
        }
    }
}