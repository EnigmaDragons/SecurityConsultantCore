using SecurityConsultantCore.Domain.Basic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SecurityConsultantCore.Domain
{
    public class FacilityLayer
    {
        private readonly List<XYOriented<FacilityObject>> _objects = new List<XYOriented<FacilityObject>>();

        public FacilityLayer() : this(64, 64)
        {
        }

        public FacilityLayer(int width, int height)
        {
            Size = new XY(width, height);
        }

        public XY Size { get; }

        public IEnumerable<XYOriented<FacilityPortal>> Portals => _objects.Where(x => x.Obj is FacilityPortal)
            .Select(y => new XYOriented<FacilityPortal>(y, (FacilityPortal)y.Obj));

        public IEnumerable<XYOriented<IValuable>> Valuables => _objects.Where(x => x.Obj is IValuable)
            .Select(y => new XYOriented<IValuable>(y, (IValuable)y.Obj));

        public bool Exists(XY xy)
        {
            return IsInBounds(xy);
        }

        public void Put(XYOrientation xyo, FacilityObject obj)
        {
            if (!IsInBounds(xyo))
                throw new InvalidOperationException("Object placed outside layer bounds");
            _objects.Add(new XYOriented<FacilityObject>(xyo, obj));
        }

        public void Remove(ValuableFacilityObject obj)
        {
            _objects.RemoveAll(x => x.Obj.Equals(obj));
        }

        private bool IsInBounds(XY xy)
        {
            return IsXInBounds(xy.X) && IsYInBounds(xy.Y);
        }

        private bool IsYInBounds(double y)
        {
            return (y > -1) && (y < Size.Y);
        }

        private bool IsXInBounds(double x)
        {
            return (x > -1) && (x < Size.X);
        }
    }
}