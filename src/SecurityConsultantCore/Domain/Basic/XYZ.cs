using System;
using SecurityConsultantCore.Common;
using SecurityConsultantCore.OOMath;
using System.Collections.Generic;
using System.Linq;

namespace SecurityConsultantCore.Domain.Basic
{
    public class XYZ : XY
    {
        private int y;

        public XYZ(Number x, Number y) 
            : this (x, y, 0) { }

        public XYZ(XY xy, Number z) 
            : this(xy.X, xy.Y, z) { }

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

        public IEnumerable<XYZ> GetNeighbors()
        {
            var neighbors = new List<XYZ>();
            for(int xOff = -1; xOff < 2; ++xOff)
                for(int yOff = -1; yOff < 2; ++yOff)
                    if (xOff != 0 || yOff != 0)
                        neighbors.Add(new XYZ(X + xOff, Y + yOff, Z));
            return neighbors;
        }
    }
}