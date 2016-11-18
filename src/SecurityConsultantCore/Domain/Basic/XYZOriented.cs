
namespace SecurityConsultantCore.Domain.Basic
{
    public class XYZOriented<T> : XYZOrientation
    {
        public XYZOriented(XYZOrientation xyzo, T obj) 
            : this(xyzo.X, xyzo.Y, xyzo.Z, xyzo.Orientation, obj)
        {
        }

        public XYZOriented(double x, double y, int z, Orientation w, T obj) : base(x, y, z, w)
        {
            Obj = obj;
        }

        public T Obj { get; }
        public XYZ Location => this;
    }
}
