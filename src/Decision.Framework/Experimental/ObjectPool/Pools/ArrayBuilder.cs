// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Decision.Framework.Experimental.ObjectPool.Pools
{
    public sealed class ArrayBuilder<T>
    {
        #region Constructors

        /// <summary>
        ///     Prevents a default instance of the <see cref="ArrayBuilder{T}" /> class from being created.
        /// </summary>
        private ArrayBuilder()
        {
            items = new List<T>();
        }

        #endregion

        #region Public Static Methods

        public static ArrayBuilder<T> GetInstance(int size = 0)
        {
            var arrayBuilder = poolInstance.Allocate();

            Debug.Assert(arrayBuilder.Count == 0);

            if (size > 0)
            {
                arrayBuilder.items.Capacity = size;
            }

            return arrayBuilder;
        }

        #endregion

        public void Add(T item)
        {
            items.Add(item);
        }

        public void AddRange(T[] items)
        {
            foreach (var item in items)
            {
                this.items.Add(item);
            }
        }

        public void AddRange(IEnumerable<T> items)
        {
            this.items.AddRange(items);
        }

        public T Peek()
        {
            return items[items.Count - 1];
        }

        public void Push(T item)
        {
            Add(item);
        }

        public T Pop()
        {
            var position = items.Count - 1;
            var result = items[position];
            items.RemoveAt(position);
            return result;
        }

        public void Clear()
        {
            items.Clear();
        }

        public void Free()
        {
            items.Clear();
            poolInstance.Free(this);
        }

        public T[] ToArray()
        {
            return items.ToArray();
        }

        public T[] ToArrayAndFree()
        {
            var result = ToArray();
            Free();
            return result;
        }

        public ReadOnlyCollection<T> ToImmutable()
        {
            return items.Count > 0 ? new ReadOnlyCollection<T>(items.ToArray()) : empty;
        }

        public ReadOnlyCollection<T> ToImmutableAndFree()
        {
            var result = ToImmutable();
            Free();
            return result;
        }

        public void Sort(IComparer<T> comparer)
        {
            items.Sort(comparer);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        #region Fields

        private static readonly ObjectPool<ArrayBuilder<T>> poolInstance =
            new ObjectPool<ArrayBuilder<T>>(() => new ArrayBuilder<T>(), 16);

        private static readonly ReadOnlyCollection<T> empty = new ReadOnlyCollection<T>(new T[0]);

        private readonly List<T> items;

        #endregion

        #region Public Properties

        public T this[int i] => items[i];

        public int Count => items.Count;

        #endregion
    }
}