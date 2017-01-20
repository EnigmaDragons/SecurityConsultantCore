using SecurityConsultantCore.Domain.Basic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SecurityConsultantCore.Spatial
{
    public class Volume
    {
        private readonly IEnumerable<XYVolume> _spaces;

        public Volume(params XYVolume[] spaces) : this ((IEnumerable<XYVolume>)spaces)
        {
        }

        public Volume(IEnumerable<XYVolume> spaces)
        {
            _spaces = spaces;
        }
        
        public override bool Equals(object obj)
        {
            var other = obj as Volume;
            if (other == null) return false;
            return _spaces.Count().Equals(other._spaces.Count()) 
                && _spaces.Union(other._spaces).Count().Equals(_spaces.Count());
        }

        public IEnumerable<XYZ> GetOccupiedTiles(XYZOrientation xyzo)
        {
            var rotated = _spaces.Where(x => x.IsSpacious).Select(xy => Rotate(xy, xyzo.Orientation));
            return rotated.Select(xy => new XYZ(xy.X + xyzo.X, xy.Y + xyzo.Y, xyzo.Z));
        }

        private XY Rotate(XY xy, Orientation orientation)
        {
            var radians = ToRadians(orientation);
            var newX = xy.X * Math.Cos(radians) - xy.Y * Math.Sin(radians);
            var newY = xy.X * Math.Sin(radians) + xy.Y * Math.Cos(radians);
            return new XY(newX, newY);
        }

        private double ToRadians(Orientation orientation)
        {
            return Math.PI * orientation.Rotation / 180.0;
        }
    }
}
