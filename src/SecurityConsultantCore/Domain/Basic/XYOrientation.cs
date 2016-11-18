using System;
using System.Linq;
using SecurityConsultantCore.Common;

namespace SecurityConsultantCore.Domain.Basic
{
    public class XYOrientation : XY
    {
        public XYOrientation(double x, double y) : this(x, y, Orientation.None)
        {
        }

        public XYOrientation(double x, double y, Orientation orientation) : base(x, y)
        {
            Orientation = orientation;
        }

        public Orientation Orientation { get; }

        protected bool Equals(XYOrientation other)
        {
            return base.Equals(other) && Equals(Orientation, other.Orientation);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return (obj.GetType() == GetType()) && Equals((XYOrientation) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (base.GetHashCode()*397) ^ (Orientation?.GetHashCode() ?? 0);
            }
        }


        public new static XYOrientation FromString(string arg)
        {
            if (string.IsNullOrEmpty(arg) || !arg.Contains(","))
                throw new ArgumentException($"Invalid input string. {arg} Expected format (x, y, orientation)");

            var parts = arg.RemoveParantheses().RemoveSpaces().Split(',');
            if (parts.Count() == 2)
                return new XYOrientation(int.Parse(parts[0]), int.Parse(parts[1]));
            return new XYOrientation(int.Parse(parts[0]), int.Parse(parts[1]), Orientation.FromAbbreviation(parts[2]));
        }
    }
}