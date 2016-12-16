﻿using System;
using System.Collections.Generic;
using System.Linq;
using SecurityConsultantCore.Common;

namespace SecurityConsultantCore.Domain.Basic
{
    public class XY
    {
        public XY()
        {
        } //For Serialization

        public XY(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double X { get; }
        public double Y { get; }

        public int XInt => (int)X;
        public int YInt => (int)Y;

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
            if (other == null)
                return false;
            return X.WithinEpsilonOf(other.X) &&
                Y.WithinEpsilonOf(other.Y);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((int)X*397) ^ (int)Y;
            }
        }

        public XY Plus(int x, int y)
        {
            return Plus(new XY(x, y));
        }

        public XY Plus(XY other)
        {
            return new XY(other.X + X, other.Y + Y);
        }

        public XY GetOffset(XY dest)
        {
            return new XY(dest.X - X, dest.Y - Y);
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
            var xDiff = Math.Abs(X - other.X);
            var yDiff = Math.Abs(Y - other.Y);
            var xAdj = xDiff == 1;
            var yAdj = yDiff == 1;
            return xAdj ? !yAdj && (yDiff == 0) : yAdj && (xDiff == 0);
        }
    }
}