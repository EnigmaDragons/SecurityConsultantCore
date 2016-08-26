using System.Collections.Generic;

namespace SecurityConsultantCore.Domain.Basic
{
    public class Tuple<T, T2>
    {
        public Tuple(T obj1, T2 obj2)
        {
            Obj1 = obj1;
            Obj2 = obj2;
        }

        public T Obj1 { get; }
        public T2 Obj2 { get; }

        protected bool Equals(Tuple<T, T2> other)
        {
            return EqualityComparer<T>.Default.Equals(Obj1, other.Obj1) &&
                   EqualityComparer<T2>.Default.Equals(Obj2, other.Obj2);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Tuple<T, T2>) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (EqualityComparer<T>.Default.GetHashCode(Obj1)*397) ^
                       EqualityComparer<T2>.Default.GetHashCode(Obj2);
            }
        }
    }
}