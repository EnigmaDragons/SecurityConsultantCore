namespace SecurityConsultantCore.Domain.Basic
{
    public class ObjectLocation<T>
    {
        public ObjectLocation(int x, int y, int z, ObjectLayer layer, T obj)
            : this(new XYZObjectLayer(x, y, z, layer), obj)
        {
        }

        public ObjectLocation(XYZObjectLayer xyzw, T obj)
        {
            Location = xyzw;
            Obj = obj;
        }

        public XYZObjectLayer Location { get; }
        public T Obj { get; }
        public int X => Location.X;
        public int Y => Location.Y;
        public int Z => Location.Z;
        public ObjectLayer Layer => Location.W;

        public new string ToString()
        {
            return $"{Obj} at {Location}";
        }
    }
}