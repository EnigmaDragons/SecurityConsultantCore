using SecurityConsultantCore.Common;
using SecurityConsultantCore.OOMath;

namespace SecurityConsultantCore.Domain.Basic
{
    public class XYZ : XY
    {
        public XYZ(XY xy, int z) : this(xy, new SimpleNumber(z)) {}

        public XYZ(double x, double y, int z) : this(new SimpleNumber(x), new SimpleNumber(y),  new SimpleNumber(y)) {}

        public XYZ(XY xy, Number z) : this(xy.X, xy.Y, z) {}

        public XYZ(Number x, Number y, Number z) : base(x, y)
        {
            Z = z;
        }

        public Number Z { get; }

        public XYZ Plus(XYZ other)
        {
            return new XYZ(base.Plus(other), new Sum(other.Z, Z));
        }

        public XYZ GetOffset(XYZ dest)
        {
            return new XYZ(base.GetOffset(dest), new Difference(dest.Z, Z));
        }

        public override string ToString()
        {
            return $"[{X}, {Y}, {Z}]";
        }

        public override bool Equals(object obj)
        {
            var p = obj as XYZ;
            return (p != null) && Equals(p);
        }

        public bool Equals(XYZ other)
        {
            return base.Equals(other) && (Z == other.Z);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ (int)Z.AsInt();
        }

        public new static XYZ FromString(string arg)
        {
            var cleanedArg = arg.RemoveParantheses().RemoveSpaces();
            var values = cleanedArg.Split(',');
            return new XYZ(int.Parse(values[0]), int.Parse(values[1]), int.Parse(values[2]));
        }
    }
}