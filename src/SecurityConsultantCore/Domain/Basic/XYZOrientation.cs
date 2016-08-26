using System;
using System.Linq;
using SecurityConsultantCore.Common;

namespace SecurityConsultantCore.Domain.Basic
{
    public class XYZOrientation : XYZW<Orientation>
    {
        public XYZOrientation(XYZ xyz, Orientation w) : base(xyz, w)
        {
        }

        public XYZOrientation(int x, int y, int z, Orientation w) : base(x, y, z, w)
        {
        }

        public Orientation Orientation => W;

        public new static XYZOrientation FromString(string arg)
        {
            if (string.IsNullOrEmpty(arg) || !arg.Contains(","))
                throw new ArgumentException($"Invalid input string. {arg} Expected format (x, y, z, orientation)");

            var parts = arg.CleanAndSplit(',');
            if (parts.Count() == 3)
                return new XYZOrientation(int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2]), Orientation.Up);
            return new XYZOrientation(int.Parse(parts[0]), int.Parse(parts[1]),
                int.Parse(parts[2]), Orientation.FromAbbreviation(parts[3]));
        }
    }
}