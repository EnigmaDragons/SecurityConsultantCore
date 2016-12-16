
using SecurityConsultantCore.Common;

namespace SecurityConsultantCore.Domain.Basic
{
    public class XYOriented<T> : XYOrientation
    {
        public XYOriented(XYOrientation xyo, T obj)
            :this(xyo.X, xyo.Y, xyo.Orientation, obj)
        {
        }

        public XYOriented(XY xy, Orientation orientation, T obj) 
            : this(xy.X, xy.Y, orientation, obj)
        {
        }

        public XYOriented(double x, double y, Orientation orientation, T obj) 
            : base(x, y, orientation)
        {
            Obj = obj;
        }

        public T Obj { get; }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (this == obj) return true;

            var other = obj as XYOriented<T>;
            if (other == null) return false;

            return Equals(other) &&
                Obj.Equals(other.Obj);
        }
    }
}
