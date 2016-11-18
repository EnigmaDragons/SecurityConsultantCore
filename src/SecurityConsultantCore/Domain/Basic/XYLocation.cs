namespace SecurityConsultantCore.Domain.Basic
{
    public class XYLocation<T>
    {
        public XYLocation(XY location, T obj)
        {
            Location = location;
            Obj = obj;
        }

        public XY Location { get; }
        public T Obj { get; private set; }
        public double X => Location.X;
        public double Y => Location.Y;
    }
}