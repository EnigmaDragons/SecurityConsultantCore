
namespace SecurityConsultantCore.Domain.Basic
{
    public class XYOriented<T> : XYOrientation
    {
        public XYOriented(XY xy, Orientation orientation, T obj) 
            : this(xy.X, xy.Y, orientation, obj)
        {
        }

        public XYOriented(int x, int y, Orientation orientation, T obj) 
            : base(x, y, orientation)
        {
            Obj = obj;
        }

        public T Obj { get; }
    }
}
