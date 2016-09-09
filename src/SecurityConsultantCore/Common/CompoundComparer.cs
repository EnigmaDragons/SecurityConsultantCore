using System.Collections.Generic;

namespace SecurityConsultantCore.Common
{
    public class CompoundComparer<T> : IComparer<T>
    {
        private readonly IComparer<T> _first;
        private readonly IComparer<T> _second;

        public CompoundComparer(IComparer<T> first, IComparer<T> second)
        {
            _first = first;
            _second = second;
        }

        public int Compare(T x, T y)
        {
            var firstResult = _first.Compare(x, y);
            return firstResult != 0 ? firstResult : _second.Compare(x, y);
        }
    }
}
