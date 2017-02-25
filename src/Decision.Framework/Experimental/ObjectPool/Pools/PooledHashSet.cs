// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics;

namespace Decision.Framework.Experimental.ObjectPool.Pools
{
    /// <summary>
    ///     Copied from Microsoft Roslyn code at
    ///     http://source.roslyn.codeplex.com/#Microsoft.CodeAnalysis/PooledHashSet.cs,afe982be5207ab5e
    ///     HashSet that can be recycled via an object pool.
    ///     NOTE: these HashSets always have the default comparer.
    /// </summary>
    internal class PooledHashSet<T> : HashSet<T>
    {
        // global pool
        private static readonly ObjectPool<PooledHashSet<T>> s_poolInstance = CreatePool();
        private readonly ObjectPool<PooledHashSet<T>> _pool;

        private PooledHashSet(ObjectPool<PooledHashSet<T>> pool)
        {
            _pool = pool;
        }

        public void Free()
        {
            Clear();
            if (_pool != null)
            {
                _pool.Free(this);
            }
        }

        // if someone needs to create a pool;
        public static ObjectPool<PooledHashSet<T>> CreatePool()
        {
            ObjectPool<PooledHashSet<T>> pool = null;
            pool = new ObjectPool<PooledHashSet<T>>(() => new PooledHashSet<T>(pool), 128);
            return pool;
        }

        public static PooledHashSet<T> GetInstance()
        {
            var instance = s_poolInstance.Allocate();
            Debug.Assert(instance.Count == 0);
            return instance;
        }
    }
}