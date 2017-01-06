using SecurityConsultantCore.Common;
using SecurityConsultantCore.Domain.Basic;
using SecurityConsultantCore.EngineInterfaces;
using SecurityConsultantCore.FacilityObjects;
using SecurityConsultantCore.MapGeneration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SecurityConsultantCore.Domain
{
    // TODO
    public class FacilityMap
    {
        private readonly List<XYZOriented<FacilityObject>> _objects = new List<XYZOriented<FacilityObject>>();
        private readonly Dictionary<XYZ, int> _occupiedSpaces = new Dictionary<XYZ, int>();
        private readonly IWorld _world;

        public FacilityMap(IWorld world, MapInstruction inst)
        {
            _world = world;
            //inst.Layers.ForEach(x => Add(LayerBuilder.Assemble(x)));
            //inst.Portals.ForEach(x => this[x.Location].Put(PortalFactory.Create(x)));
        }

        public FacilityMap(IWorld world)
        {
            _world = world;
        }

        public void Put(XYZOrientation xyzo, FacilityObject obj)
        {
            _objects.Add(new XYZOriented<FacilityObject>(xyzo, obj));
            obj.Volume.GetOccupiedSpaces(xyzo).ForEach(x => UpdateOccupiedSpaces(x, 1));
        }

        public IEnumerable<XYZOriented<FacilityObject>> ObjectsOnSpace(XYZ xyz)
        {
            return ObjectsOnSpace(xyz.X, xyz.Y, xyz.Z);
        }

        public IEnumerable<XYZOriented<FacilityObject>> ObjectsOnSpace(double x, double y, int z)
        {
            return _objects.Where(o => InSpace(x, y, z, o));
        }

        public IEnumerable<SpatialValuable> SpatialValuables => _objects.Where(x => x.Obj is IValuable).Select(x => new SpatialValuable(x, (IValuable)x.Obj));

        public IEnumerable<XYZLocation<FacilityPortal>> Portals => _objects.Where(x => x.Obj is FacilityPortal).Select(x => new XYZLocation<FacilityPortal>(x, (FacilityPortal)x.Obj));
        
        public void Remove(IValuable valuable)
        {
            var found = _objects.FirstOrDefault(xyzo => xyzo.Obj.Equals(valuable));
            _objects.RemoveAll(xyzo => xyzo.Obj.Equals(valuable));
            if (!(found == null))
                found.Obj.Volume.GetOccupiedSpaces(found).ForEach(x => UpdateOccupiedSpaces(x, -1));
        }

        private bool InSpace(double x, double y, int z, XYZ location)
        {
            return location.X >= x &&
                location.X < x + 1.0 &&
                location.Y >= y &&
                location.Y < y + 1.0 &&
                location.Z == z;
        }

        public bool IsOpen(XYZ xyz)
        {
            return GetVolumetricObjectCount(xyz).Equals(0);
        }

        public bool Exists(XYZ xyz)
        {
            return true;
        }

        public void ShowLayer(int layer)
        {
            //_world.HideEverything();
            //_layers[layer].ForEach(space => _world.Show(space.Obj, new XYZ(space.Location, layer)));
        }

        private int GetVolumetricObjectCount(XYZ xyz)
        {
            if (!_occupiedSpaces.ContainsKey(xyz))
                _occupiedSpaces[xyz] = 0;
            return _occupiedSpaces[xyz];
        }

        private void SetVolumetricObjectCount(XYZ xyz, int numObjs)
        {
            _occupiedSpaces[xyz] = numObjs;
        }

        private void UpdateOccupiedSpaces(XYZ xyz, int modifier)
        {
            SetVolumetricObjectCount(xyz, GetVolumetricObjectCount(xyz) + modifier);
        }
    }
}
