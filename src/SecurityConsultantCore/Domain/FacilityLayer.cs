using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SecurityConsultantCore.Common;
using SecurityConsultantCore.Domain.Basic;

namespace SecurityConsultantCore.Domain
{
    public class FacilityLayer : IEnumerable<XYLocation<FacilitySpace>>
    {
        private readonly FacilitySpace[,] _spaces;

        public FacilityLayer() : this(64, 64)
        {
        }

        public FacilityLayer(int width, int height)
        {
            _spaces = new FacilitySpace[width, height];
        }

        public XY Size => new XY(_spaces.GetLength(0), _spaces.GetLength(1));
        public FacilitySpace this[XY xy] => Get(xy.X, xy.Y);
        public FacilitySpace this[int x, int y] => Get(x, y);

        public IEnumerable<XYOriented<IValuable>> OrientedValuables => this.SelectMany(x => x.Obj.OrientedValuables
            .Select(y => new XYOriented<IValuable>(x.Location, y.Orientation, y.Obj)));

        public IEnumerable<XYLocation<FacilityPortal>> Portals => this.SelectMany(x => x.Obj.Portals
            .Select(y => new XYLocation<FacilityPortal>(x.Location, y)));

        public IEnumerator<XYLocation<FacilitySpace>> GetEnumerator()
        {
            return Size.Equals(new XY(0, 0)) 
                ? new List<XYLocation<FacilitySpace>>().GetEnumerator() 
                : new XYRange(new XY(0, 0), Size.Plus(-1, -1)).Select(xy => new XYLocation<FacilitySpace>(xy, this[xy])).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Put(int x, int y, FacilitySpace space)
        {
            _spaces[x, y] = space;
        }

        public void Put(XY location, FacilitySpace space)
        {
            Put(location.X, location.Y, space);
        }

        public IEnumerable<XYLocation<FacilitySpace>> GetNeighbors(XY xy)
        {
            return xy.Plus(-1, -1).Thru(xy.Plus(1, 1))
                .Where(x => IsInBounds(x) && !x.Equals(xy))
                .Select(GetLocationSpace);
        }

        public IEnumerable<XYLocation<FacilitySpace>> GetAdjacentLocations(XY xy)
        {
            return xy.Plus(-1, -1).Thru(xy.Plus(1, 1))
                    .Where(x => (Math.Abs(x.X) + Math.Abs(x.Y)).Equals(1))
                    .Select(GetLocationSpace);
        }

        public void Remove(IValuable valuable)
        {
            RemoveValuable(valuable);
        }

        public void Remove(ValuableFacilityObject valuable)
        {
            RemoveValuable(valuable);
            valuable.LinkedObjs.Where(x => x is IValuable).Cast<IValuable>().ForEach(RemoveValuable);
        }

        private void RemoveValuable(IValuable valuable)
        {
            OrientedValuables.Where(x => x.Obj.Equals(valuable)).ForEach(y => this[y].Remove(valuable));
        }

        public bool Exists(XY xy)
        {
            return IsInBounds(xy);
        }

        private FacilitySpace Get(int x, int y)
        {
            return _spaces[x, y] ?? (_spaces[x, y] = new FacilitySpace());
        }

        private bool IsInBounds(XY xy)
        {
            return IsXInBounds(xy.X) && IsYInBounds(xy.Y);
        }

        private bool IsYInBounds(int y)
        {
            return (y > -1) && (y < Size.Y);
        }

        private bool IsXInBounds(int x)
        {
            return (x > -1) && (x < Size.X);
        }

        private XYLocation<FacilitySpace> GetLocationSpace(XY xy)
        {
            return new XYLocation<FacilitySpace>(xy, this[xy]);
        }
    }
}