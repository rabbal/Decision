// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics;

namespace Decision.Common.Experimental.ObjectPool.Pools
{
    /// <summary>
    /// Copied from Microsoft Roslyn code at http://source.roslyn.codeplex.com/#Microsoft.CodeAnalysis/PooledDictionary.cs,ebb1ac303c777646
    /// Dictionary that can be recycled via an object pool.
    /// NOTE: these dictionaries always have the default comparer.
    /// </summary>
    internal class PooledDictionary<TK, TV> : Dictionary<TK, TV>
    {
        private readonly ObjectPool<PooledDictionary<TK, TV>> _pool;

        private PooledDictionary(ObjectPool<PooledDictionary<TK, TV>> pool)
        {
            _pool = pool;
        }

        public void Free()
        {
            this.Clear();
            if (_pool != null)
            {
                _pool.Free(this);
            }
        }

        // global pool
        private static readonly ObjectPool<PooledDictionary<TK, TV>> SPoolInstance = CreatePool();

        // if someone needs to create a pool;
        public static ObjectPool<PooledDictionary<TK, TV>> CreatePool()
        {
            ObjectPool<PooledDictionary<TK, TV>> pool = null;
            pool = new ObjectPool<PooledDictionary<TK, TV>>(() => new PooledDictionary<TK, TV>(pool), 128);
            return pool;
        }

        public static PooledDictionary<TK, TV> GetInstance()
        {
            var instance = SPoolInstance.Allocate();
            Debug.Assert(instance.Count == 0);
            return instance;
        }
    }
}