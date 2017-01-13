using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SecurityConsultantCore.Domain.Basic;
using SecurityConsultantCore.MapGeneration;
using SecurityConsultantCore.Factories;
using SecurityConsultantCore.Common;
using SecurityConsultantCore.Engine;

namespace SecurityConsultantCore.Domain
{
    public class FacilityMap : IEnumerable<ZLocation<FacilityLayer>>
    {
        private readonly List<FacilityLayer> _layers = new List<FacilityLayer>();
        private readonly IWorld _world;

        public FacilityMap(IWorld world, MapInstruction inst)
        {
            _world = world;
            inst.Layers.ForEach(x => Add(LayerBuilder.Assemble(x)));
            inst.Portals.ForEach(x => this[x.Location].Put(PortalFactory.Create(x)));
        }

        public FacilityMap(IWorld world)
        {
            _world = world;
        }

        public int LayerCount => _layers.Count;

        public FacilityLayer this[int z] => _layers[z];
        public FacilitySpace this[int x, int y, int z] => _layers[z][x, y];
        public FacilitySpace this[XYZ xyz] => _layers[xyz.Z][xyz.X, xyz.Y];

        public IEnumerable<SpatialValuable> SpatialValuables => this.SelectMany(z => z.Obj.OrientedValuables
                .Select(xy => new SpatialValuable(new XYZ(xy.X, xy.Y, z.Z), xy.Orientation, xy.Obj)));

        public IEnumerable<XYZLocation<FacilityPortal>> Portals => this.SelectMany(z => z.Obj.Portals
                .Select(xy => new XYZLocation<FacilityPortal>(new XYZ(xy.X, xy.Y, z.Z), xy.Obj)));

        public IEnumerator<ZLocation<FacilityLayer>> GetEnumerator()
        {
            for (var z = 0; z < LayerCount; z++)
                yield return new ZLocation<FacilityLayer>(z, _layers[z]);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        public void Remove(IValuable valuable)
        {
            _layers.ForEach(x => x.Remove(valuable));
        }

        public void Add(FacilityLayer layer)
        {
            _layers.Add(layer);
        }

        //TODO: test this
        public bool IsOpenSpace(XYZ xyz)
        {
            return Exists(xyz) && this[xyz].IsOpenSpace;
        }

        //TODO: test this
        public IEnumerable<XYZLocation<FacilitySpace>> GetAdjacentLocations(XYZ xyz)
        {
            return _layers[xyz.Z].GetAdjacentLocations(new XY(xyz.X, xyz.Y))
                .Select(x => new XYZLocation<FacilitySpace>(new XYZ(x.X, x.Y, xyz.Z), x.Obj));
        }

        public bool Exists(XYZ xyz)
        {
            return IsInZBounds(xyz) && _layers[xyz.Z].Exists(xyz);
        }

        private bool IsInZBounds(XYZ xyz)
        {
            return xyz.Z >= 0 && _layers.Count > xyz.Z;
        }

        public void ShowLayer(int layer)
        {
            _world.HideEverything();
            _layers[layer].ForEach(space => _world.Show(space.Obj, new XYZ(space.Location, layer)));
        }
    }
}
