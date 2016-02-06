// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Decision.Common.Experimental.ObjectPool.Pools
{
    public sealed class ArrayBuilder<T>
    {
        #region Fields

        private static readonly ObjectPool<ArrayBuilder<T>> PoolInstance = new ObjectPool<ArrayBuilder<T>>(() => new ArrayBuilder<T>(), 16);
        private static readonly ReadOnlyCollection<T> Empty = new ReadOnlyCollection<T>(new T[0]);

        private readonly List<T> _items;

        #endregion

        #region Constructors

        /// <summary>
        /// Prevents a default instance of the <see cref="ArrayBuilder{T}"/> class from being created.
        /// </summary>
        private ArrayBuilder()
        {
            this._items = new List<T>();
        }

        #endregion

        #region Public Static Methods

        public static ArrayBuilder<T> GetInstance(int size = 0)
        {
            var arrayBuilder = PoolInstance.Allocate();

            Debug.Assert(arrayBuilder.Count == 0);

            if (size > 0)
            {
                arrayBuilder._items.Capacity = size;
            }

            return arrayBuilder;
        }

        #endregion

        #region Public Properties

        public T this[int i]
        {
            get { return this._items[i]; }
        }

        public int Count
        {
            get { return this._items.Count; }
        } 

        #endregion

        public void Add(T item)
        {
            this._items.Add(item);
        }

        public void AddRange(T[] items)
        {
            foreach (var item in items)
            {
                this._items.Add(item);
            }
        }

        public void AddRange(IEnumerable<T> items)
        {
            this._items.AddRange(items);
        }

        public T Peek()
        {
            return this._items[this._items.Count - 1];
        }
        
        public void Push(T item)
        {
            this.Add(item);
        }

        public T Pop()
        {
            var position = this._items.Count - 1;
            var result = this._items[position];
            this._items.RemoveAt(position);
            return result;
        }

        public void Clear()
        {
            this._items.Clear();
        }

        public void Free()
        {
            this._items.Clear();
            PoolInstance.Free(this);
        }

        public T[] ToArray()
        {
            return this._items.ToArray();
        }

        public T[] ToArrayAndFree()
        {
            var result = this.ToArray();
            this.Free();
            return result;
        }

        public ReadOnlyCollection<T> ToImmutable()
        {
            return (this._items.Count > 0) ? new ReadOnlyCollection<T>(this._items.ToArray()) : Empty;
        }

        public ReadOnlyCollection<T> ToImmutableAndFree()
        {
            var result = this.ToImmutable();
            this.Free();
            return result;
        }

        public void Sort(IComparer<T> comparer)
        {
            this._items.Sort(comparer);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this._items.GetEnumerator();
        }
    }
}
