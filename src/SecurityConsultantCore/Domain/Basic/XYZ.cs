using SecurityConsultantCore.Common;

namespace SecurityConsultantCore.Domain.Basic
{
    public class XYZ : XY
    {
        public XYZ()
        {
        } //For Serialization

        public XYZ(XY xy, int z) : base(xy.X, xy.Y)
        {
            Z = z;
        }

        public XYZ(int x, int y, int z) : base(x, y)
        {
            Z = z;
        }

        public int Z { get; }

        public XYZ Plus(XYZ other)
        {
            return new XYZ(other.X + X, other.Y + Y, other.Z + Z);
        }

        public XYZ GetOffset(XYZ dest)
        {
            return new XYZ(dest.X - X, dest.Y - Y, dest.Z - Z);
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
            return base.GetHashCode() ^ Z;
        }

        public new static XYZ FromString(string arg)
        {
            var cleanedArg = arg.RemoveParantheses().RemoveSpaces();
            var values = cleanedArg.Split(',');
            return new XYZ(int.Parse(values[0]), int.Parse(values[1]), int.Parse(values[2]));
        }
    }
}