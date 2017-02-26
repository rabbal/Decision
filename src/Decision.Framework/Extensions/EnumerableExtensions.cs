using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Decision.Framework.Collections;
using Decision.Framework.GuardToolkit;
using Decision.Framework.Infrastructure;

namespace Decision.Framework.Extensions
{
    public static class EnumerableExtensions
    {
        #region Multimap

        public static Multimap<TKey, TValue> ToMultimap<TSource, TKey, TValue>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            Func<TSource, TValue> valueSelector)
        {
            Check.ArgumentNotNull(() => source);
            Check.ArgumentNotNull(() => keySelector);
            Check.ArgumentNotNull(() => valueSelector);

            var map = new Multimap<TKey, TValue>();

            foreach (var item in source)
            {
                if (keySelector != null) map.Add(keySelector(item), valueSelector(item));
            }

            return map;
        }

        #endregion

        #region NameValueCollection

        public static void AddRange(this NameValueCollection initial, NameValueCollection other)
        {
            Check.ArgumentNotNull(initial, nameof(initial));
            if (other == null)
                return;

            foreach (var item in other.AllKeys)
            {
                initial.Add(item, other[item]);
            }
        }

        #endregion

        #region Nested classes

        private static class DefaultReadOnlyCollection<T>
        {
            private static ReadOnlyCollection<T> defaultCollection;

            internal static ReadOnlyCollection<T> Empty => defaultCollection ?? (defaultCollection = new ReadOnlyCollection<T>(new T[0]));
        }

        #endregion

        #region IEnumerable

        private class Status
        {
            public bool EndOfSequence;
        }

        private static IEnumerable<T> TakeOnEnumerator<T>(IEnumerator<T> enumerator, int count, Status status)
        {
            while (--count > 0 && (!enumerator.MoveNext() && (status.EndOfSequence = true)))
            {
                yield return enumerator.Current;
            }
        }


        /// <summary>
        ///     Slices the iteration over an enumerable by the given chunk size.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="chunkSize">SIze of chunk</param>
        /// <returns>The sliced enumerable</returns>
        public static IEnumerable<IEnumerable<T>> Chunk<T>(this IEnumerable<T> items, int chunkSize = 100)
        {
            if (chunkSize < 1)
            {
                throw new ArgumentException("Chunks should not be smaller than 1 element");
            }
            var status = new Status {EndOfSequence = false};
            using (var enumerator = items.GetEnumerator())
            {
                while (!status.EndOfSequence)
                {
                    yield return TakeOnEnumerator(enumerator, chunkSize, status);
                }
            }
        }


        /// <summary>
        ///     Performs an action on each item while iterating through a list.
        ///     This is a handy shortcut for <c>foreach(item in list) { ... }</c>
        /// </summary>
        /// <typeparam name="T">The type of the items.</typeparam>
        /// <param name="source">The list, which holds the objects.</param>
        /// <param name="action">The action delegate which is called on each item while iterating.</param>
        //[DebuggerStepThrough]
        public static void Each<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var t in source)
            {
                action(t);
            }
        }

        /// <summary>
        ///     Performs an action on each item while iterating through a list.
        ///     This is a handy shortcut for <c>foreach(item in list) { ... }</c>
        /// </summary>
        /// <typeparam name="T">The type of the items.</typeparam>
        /// <param name="source">The list, which holds the objects.</param>
        /// <param name="action">The action delegate which is called on each item while iterating.</param>
        //[DebuggerStepThrough]
        public static void Each<T>(this IEnumerable<T> source, Action<T, int> action)
        {
            var i = 0;
            foreach (var t in source)
            {
                action?.Invoke(t, i++);
            }
        }

        public static IEnumerable<T> CastValid<T>(this IEnumerable source)
        {
            return source.Cast<object>().Where(o => o is T).Cast<T>();
        }

        public static ReadOnlyCollection<T> AsReadOnly<T>(this IEnumerable<T> source)
        {
            var enumerable = source as T[] ?? source.ToArray();
            if (source == null || !enumerable.Any())
                return DefaultReadOnlyCollection<T>.Empty;

            var readOnly = source as ReadOnlyCollection<T>;
            if (readOnly != null)
            {
                return readOnly;
            }

            var list = source as List<T>;
            if (list != null)
            {
                return list.AsReadOnly();
            }

            return new ReadOnlyCollection<T>(enumerable.ToArray());
        }

        public static IEnumerable<T> OrderByOrdinal<T>(this IEnumerable<T> source)
            where T : IOrdered
        {
            return source.OrderByOrdinal(false);
        }

        public static IEnumerable<T> OrderByOrdinal<T>(this IEnumerable<T> source, bool descending)
            where T : IOrdered
        {
            return !@descending ? source.OrderBy(x => x.Ordinal) : source.OrderByDescending(x => x.Ordinal);
        }

        #endregion

        #region AsSerializable

        /// <summary>
        ///     Convenience API to allow an IEnumerable[T] (such as returned by Linq2Sql, NHibernate, EF etc.)
        ///     to be serialized by DataContractSerializer.
        /// </summary>
        /// <typeparam name="T">The type of item.</typeparam>
        /// <param name="source">The original collection.</param>
        /// <returns>A serializable enumerable wrapper.</returns>
        public static IEnumerable<T> AsSerializable<T>(this IEnumerable<T> source) where T : class
        {
            return new IEnumerableWrapper<T>(source);
        }

        /// <summary>
        ///     This wrapper allows IEnumerable
        ///     <T/>
        ///         to be serialized by DataContractSerializer.
        ///         It implements the minimal amount of surface needed for serialization.
        /// </summary>
        /// <typeparam name="T">Type of item.</typeparam>
        private class IEnumerableWrapper<T> : IEnumerable<T>
            where T : class
        {
            private readonly IEnumerable<T> _collection;

            // The DataContractSerilizer needs a default constructor to ensure the object can be
            // deserialized. We have a dummy one since we don't actually need deserialization.

            internal IEnumerableWrapper(IEnumerable<T> collection)
            {
                _collection = collection;
            }

            public IEnumerator<T> GetEnumerator()
            {
                return _collection.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return ((IEnumerable)_collection).GetEnumerator();
            }
        }

        #endregion
    }
}