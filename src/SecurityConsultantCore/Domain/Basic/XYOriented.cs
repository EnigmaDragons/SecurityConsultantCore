
namespace SecurityConsultantCore.Domain.Basic
{
    public class XYOriented<T> : XYOrientation
    {
        private IValuable obj;
        private XYOriented<FacilityObject> y;

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
    }
}
