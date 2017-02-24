using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Decision.Common.Extensions;

namespace Decision.Common.Infrastructure
{
    public class MostRecentlyUsedList<T> : IEnumerable<T>
    {
        private readonly int _maxSize;
        private readonly List<T> _mru;

        public MostRecentlyUsedList(int maxSize)
        {
            _maxSize = maxSize;
            _mru = new List<T>();
        }

        public MostRecentlyUsedList(IEnumerable<T> collection, int maxSize)
        {
            _maxSize = maxSize;
            _mru = collection.ToList();

            Normalize();
        }

        public MostRecentlyUsedList(string collection, int maxSize)
        {
            _maxSize = maxSize;
            _mru = (collection.SplitSafe(Delimiter) as IEnumerable<T>).ToList();

            Normalize();
        }

        public MostRecentlyUsedList(IEnumerable<T> collection, T newItem, int maxSize)
        {
            _maxSize = maxSize;
            _mru = collection.ToList();

            Add(newItem);
        }

        public MostRecentlyUsedList(string collection, T newItem, int maxSize)
        {
            _maxSize = maxSize;
            _mru = (collection.SplitSafe(Delimiter) as IEnumerable<T>).ToList();

            Add(newItem);
        }

        public static string Delimiter => ";";

        public T this[int key]
        {
            get { return _mru[key]; }
            set { _mru[key] = value; }
        }

        public int Count => _mru.Count;

        public IEnumerator<T> GetEnumerator()
        {
            return _mru.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _mru.GetEnumerator();
        }

        private void Normalize()
        {
            if (_maxSize >= 0)
            {
                while (_mru.Count > _maxSize)
                    _mru.RemoveAt(_mru.Count - 1);
            }
        }

        public override string ToString()
        {
            return string.Join(Delimiter, _mru);
        }

        public void Add(T item)
        {
            var i = _mru.IndexOf(item);
            if (i > -1)
                _mru.RemoveAt(i);

            _mru.Insert(0, item);

            Normalize();
        }
    }
}