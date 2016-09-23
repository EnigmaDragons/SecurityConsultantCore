using System.Collections.Generic;

namespace SecurityConsultantCore.Domain.Basic
{
    public class XYZAdjacent : XYZ
    {
        private static readonly Dictionary<int, XY> Offsets = new Dictionary<int, XY>
        {
            { 0, new XY(0, -1) },
            { 90, new XY(-1, 0) },
            { 180, new XY(0, 1) },
            { 270, new XY(1, 0) }
        };

        public XYZAdjacent(XYZ xyz, Orientation directionFromSrc) 
            : base(xyz.Plus(Offsets[directionFromSrc.Rotation]), xyz.Z)
        {
        }
    }
}
