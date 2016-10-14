using System;
using System.Collections;
using System.Collections.Generic;

namespace SecurityConsultantCore.Common
{
    public class ObservableList<T> : IList<T>
    {
        private List<T> _list;
        private readonly Action<IEnumerable<T>> _onChange;

        public ObservableList(Action<IEnumerable<T>> onChange) : this(new List<T>(), onChange) {}

        public ObservableList(List<T> list, Action<IEnumerable<T>> onChange)
        {
            _list = list;
            _onChange = onChange;
        }

        public T this[int index]
        {
            get { return _list[index]; }
            set
            {
                _list[index] = value;
                _onChange(this);
            }
        }

        public int Count { get { return _list.Count; } }

        public bool IsReadOnly { get { return false; } }

        public void Add(T item)
        {
            _list.Add(item);
            _onChange(this);
        }

        public void Clear()
        {
            _list.Clear();
            _onChange(this);
        }

        public bool Contains(T item)
        {
            return _list.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _list.CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        public int IndexOf(T item)
        {
            return _list.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            _list.Insert(index, item);
            _onChange(this);
        }

        public bool Remove(T item)
        {
            var removed = _list.Remove(item);
            _onChange(this);
            return removed;
        }

        public void RemoveAt(int index)
        {
            _list.RemoveAt(index);
            _onChange(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _list.GetEnumerator();
        }
    }
}
