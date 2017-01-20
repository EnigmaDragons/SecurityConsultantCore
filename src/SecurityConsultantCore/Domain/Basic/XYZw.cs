using System.Collections.Generic;
using SecurityConsultantCore.OOMath;

namespace SecurityConsultantCore.Domain.Basic
{
    public class XYZW<T> : XYZ
    {
        public XYZW(XYZ xyz, T w) : base(xyz.X, xyz.Y, xyz.Z)
        {
            W = w;
        }
        
        public XYZW(Number x, Number y, Number z, T w) : base(x, y, z)
        {
            W = w;
        }

        public T W { get; }

        public override string ToString()
        {
            return $"[{X}, {Y}, {Z}, {W}]";
        }

        public override bool Equals(object obj)
        {
            var p = obj as XYZW<T>;
            return (p != null) && Equals(p);
        }

        protected bool Equals(XYZW<T> other)
        {
            return base.Equals(other) && EqualityComparer<T>.Default.Equals(W, other.W);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (base.GetHashCode()*397) ^ EqualityComparer<T>.Default.GetHashCode(W);
            }
        }
    }
}