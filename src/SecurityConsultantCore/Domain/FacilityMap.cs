using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SecurityConsultantCore.Domain.Basic;

namespace SecurityConsultantCore.Domain
{
    public class FacilityMap : IEnumerable<ZLocation<FacilityLayer>>, IValuablesContainer
    {
        private readonly List<FacilityLayer> _layers = new List<FacilityLayer>();

        public int LayerCount => _layers.Count;

        public FacilityLayer this[int z] => _layers[z];
        public FacilitySpace this[int x, int y, int z] => _layers[z][x, y];
        public FacilitySpace this[XYZ xyz] => _layers[xyz.Z][xyz.X, xyz.Y];

        public IEnumerable<SpatialValuable> SpatialValuables
        {
            get
            {
                return this.SelectMany(z => z.Obj.OrientedValuables.Select(xy =>
                        new SpatialValuable(new XYZ(xy.X, xy.Y, z.Z), xy.Orientation, xy.Obj)));
            }
        }

        public IEnumerable<XYZLocation<FacilityPortal>> Portals
        {
            get
            {
                return this.SelectMany(z => z.Obj.Portals.Select(xy =>
                        new XYZLocation<FacilityPortal>(new XYZ(xy.X, xy.Y, z.Z), xy.Obj)));
            }
        }

        public IEnumerator<ZLocation<FacilityLayer>> GetEnumerator()
        {
            for (var z = 0; z < LayerCount; z++)
                yield return new ZLocation<FacilityLayer>(z, _layers[z]);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerable<IValuable> Valuables => _layers.SelectMany(x => x.Valuables);

        public void Remove(IValuable valuable)
        {
            _layers.ForEach(x => x.Remove(valuable));
        }

        public void Add(FacilityLayer layer)
        {
            _layers.Add(layer);
        }

        public bool Exists(XYZ location)
        {
            if ((_layers.Count > location.Z) && (location.Z >= 0))
                return _layers[location.Z].Exists(new XY(location.X, location.Y));
            return false;
        }

        //TODO: test this
        public bool IsOpenSpace(XYZ location)
        {
            return Exists(location) && this[location].IsOpenSpace();
        }

        //TODO: test this
        public List<XYZLocation<FacilitySpace>> GetAdjacentLocations(XYZ location)
        {
            return _layers[location.Z].GetAdjacentLocations(new XY(location.X, location.Y))
                .Select(x => new XYZLocation<FacilitySpace>(new XYZ(x.X, x.Y, location.Z), x.Obj)).ToList();
        }
    }
}