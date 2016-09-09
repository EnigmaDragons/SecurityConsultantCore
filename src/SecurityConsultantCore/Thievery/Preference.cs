using System;
using System.Collections.Generic;
using SecurityConsultantCore.Domain;

namespace SecurityConsultantCore.Thievery
{
    public class Preference : IComparer<IValuable>
    {
        private readonly Func<IValuable, IValuable, int> _comparer;

        public Preference() : this((x1, x2) => 0)
        {
        }

        public Preference(Func<IValuable, IValuable, int> comparer)
        {
            _comparer = comparer;
        }

        public int Compare(IValuable x, IValuable y)
        {
            return _comparer.Invoke(x, y);
        }
    }
}
