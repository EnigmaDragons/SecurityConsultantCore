namespace SecurityConsultantCore.Domain.Basic
{
    public class XYZLocation<T>
    {
        public XYZLocation(XYZ location, T obj)
        {
            Location = location;
            Obj = obj;
        }

        public XYZ Location { get; }
        public T Obj { get; }
        public double X => Location.X;
        public double Y => Location.Y;
        public int Z => Location.Z;
    }
}