using System.Collections.Generic;

namespace SecurityConsultantCore.Common
{
    public abstract class ObservedBase<T> : Observed<T>
    {
        private List<Observer<T>> _observers = new List<Observer<T>>();

        public void Subscribe(Observer<T> observer)
        {
            _observers.Add(observer);
        }

        public void Unsubscribe(Observer<T> observer)
        {
            _observers.Remove(observer);
        }

        protected void NotifySubscribers(T obj)
        {
            _observers.ForEach(x => x.Update(obj));
        }
    }
}
