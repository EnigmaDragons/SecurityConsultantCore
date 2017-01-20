using System;
using System.Collections.Generic;
using System.Linq;
using SecurityConsultantCore.Common;
using SecurityConsultantCore.OOMath;

namespace SecurityConsultantCore.Domain.Basic
{
    public class XY
    {
        public XY() : this(0, 0) {}
        
        public XY(Number x, Number y)
        {
            X = x;
            Y = y;
        }

        public Number X { get; }
        public Number Y { get; }

        public override string ToString()
        {
            return X + ", " + Y;
        }

        public override bool Equals(object obj)
        {
            var p = obj as XY;
            return (p != null) && Equals(p);
        }

        public bool Equals(XY other)
        {
            if (other == null) return false;
            return X.Equals(other.X) && Y.Equals(other.Y);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((int)X.AsInt()*397) ^ (int)Y.AsInt();
            }
        }

        public XY Plus(int x, int y)
        {
            return Plus(new XY(x, y));
        }

        public XY Plus(XY other)
        {
            return new XY(new Sum(X, other.X), new Sum(Y, other.Y));
        }

        public XY GetOffset(XY dest)
        {
            return new XY(new Difference(dest.X, X), new Difference(dest.Y, Y));
        }

        public List<XY> Thru(XY end)
        {
            return new XYRange(this, end).ToList();
        }

        public static XY FromString(string arg)
        {
            var cleanedArg = arg.RemoveParantheses().RemoveSpaces();
            var values = cleanedArg.Split(',');
            return new XY(int.Parse(values[0]), int.Parse(values[1]));
        }

        public bool IsAdjacentTo(XY other)
        {
            var xDiff = new Absolute(new Difference(X, other.X)).AsInt();
            var yDiff = new Absolute(new Difference(Y, other.Y)).AsInt();
            var xAdj = xDiff == 1;
            var yAdj = yDiff == 1;
            return xAdj ? !yAdj && (yDiff == 0) : yAdj && (xDiff == 0);
        }
    }
}