using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SecurityConsultantCore.Domain.Basic;

namespace SecurityConsultantCore.Domain
{
    public class FacilityLayer : IEnumerable<XYLocation<FacilitySpace>>, IValuablesContainer
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

        public IEnumerable<IValuable> Valuables => this.SelectMany(x => x.Obj.Valuables);

        public IEnumerable<XYLocation<FacilityPortal>> Portals => this.SelectMany(x => x.Obj.Portals
            .Select(y => new XYLocation<FacilityPortal>(x.Location, y)));

        public IEnumerator<XYLocation<FacilitySpace>> GetEnumerator()
        {
            for (var row = 0; row < Size.Y; row++)
                for (var col = 0; col < Size.X; col++)
                    yield return new XYLocation<FacilitySpace>(new XY(col, row), this[col, row]);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Remove(IValuable valuable)
        {
            RemoveValuable(valuable);
        }

        public void Remove(ValuableFacilityObject valuable)
        {
            RemoveValuable(valuable);
            valuable.LinkedObjs.Where(x => x is IValuable).Cast<IValuable>().ToList()
                .ForEach(RemoveValuable);
        }

        public void Put(int x, int y, FacilitySpace space)
        {
            _spaces[x, y] = space;
        }

        public void Put(XY location, FacilitySpace space)
        {
            Put(location.X, location.Y, space);
        }

        public List<XYLocation<FacilitySpace>> GetNeighbors(XY location)
        {
            var neighbors = new List<XYLocation<FacilitySpace>>();
            for (var x = location.X - 1; x < location.X + 2; x++)
                for (var y = location.Y - 1; y < location.Y + 2; y++)
                    if (IsInBounds(x, y) && !new XY(x, y).Equals(location))
                        neighbors.Add(new XYLocation<FacilitySpace>(new XY(x, y), this[x, y]));
            return neighbors;
        }

        public List<XYLocation<FacilitySpace>> GetAdjacentLocations(XY location)
        {
            var list = new List<XYLocation<FacilitySpace>>();
            if (IsInBounds(location.X - 1, location.Y))
                list.Add(GetLocationSpace(new XY(location.X - 1, location.Y)));
            if (IsInBounds(location.X + 1, location.Y))
                list.Add(GetLocationSpace(new XY(location.X + 1, location.Y)));
            if (IsInBounds(location.X, location.Y - 1))
                list.Add(GetLocationSpace(new XY(location.X, location.Y - 1)));
            if (IsInBounds(location.X, location.Y + 1))
                list.Add(GetLocationSpace(new XY(location.X, location.Y + 1)));
            return list;
        }

        private void RemoveValuable(IValuable valuable)
        {
            OrientedValuables.Where(x => x.Obj.Equals(valuable)).ToList()
                .ForEach(y => this[y].Remove(valuable));
        }

        public bool Exists(XY space)
        {
            return IsInBounds(space.X, space.Y);
        }

        private FacilitySpace Get(int x, int y)
        {
            return _spaces[x, y] ??
                   (_spaces[x, y] = new FacilitySpace());
        }

        private bool IsInBounds(int x, int y)
        {
            return IsXInBounds(x) && IsYInBounds(y);
        }

        private bool IsYInBounds(int y)
        {
            return (y > -1) && (y < Size.Y);
        }

        private bool IsXInBounds(int x)
        {
            return (x > -1) && (x < Size.X);
        }

        private XYLocation<FacilitySpace> GetLocationSpace(XY target)
        {
            return new XYLocation<FacilitySpace>(target, this[target]);
        }
    }
}