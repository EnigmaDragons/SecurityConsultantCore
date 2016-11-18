using System.Collections.Generic;
using System.Linq;

namespace SecurityConsultantCore.Common
{
    public class SimpleObserver<T> : Observer<T>
    {
        private List<T> _updates = new List<T>();

        public void Update(T obj)
        {
            _updates.Add(obj);
        }

        public T LastUpdate => _updates.Last();

        public int UpdateCount => _updates.Count;
    }
}
