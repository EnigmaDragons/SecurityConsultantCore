using SecurityConsultantCore.Domain.Basic;
using SecurityConsultantCore.EngineInterfaces;
using SecurityConsultantCore.FacilityObjects;
using SecurityConsultantCore.MapGeneration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SecurityConsultantCore.Domain
{
    public class FacilityMap
    {
        private List<XYZOriented<FacilityObject>> _objects = new List<XYZOriented<FacilityObject>>();
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
        }

        public IEnumerable<XYZOriented<FacilityObject>> ObjectsOnSpace(XYZ xyz)
        {
            return ObjectsOnSpace(xyz.X, xyz.Y, xyz.Z);
        }

        public IEnumerable<XYZOriented<FacilityObject>> ObjectsOnSpace(double x, double y, int z)
        {
            return _objects.Where(o => InSpace(x, y, z, o));
        }

        public IEnumerable<SpatialValuable> SpatialValuables => new List<SpatialValuable>();//this.SelectMany(zloc => zloc.Obj.OrientedValuables
                                                                    //.Select(xy => new SpatialValuable(new XYZ(xy.X, xy.Y, zloc.Z), xy.Orientation, xy.Obj)));

        public IEnumerable<XYZLocation<FacilityPortal>> Portals => new List<XYZLocation<FacilityPortal>>();//this.SelectMany(z => z.Obj.Portals
            //.Select(xy => new XYZLocation<FacilityPortal>(new XYZ(xy.X, xy.Y, z.Z), xy.Obj)));
        
        public void Remove(IValuable valuable)
        {
            //_layers.ForEach(x => x.Remove(valuable));
        }

        private bool InSpace(double x, double y, int z, XYZ location)
        {
            return location.X >= x &&
                location.X < x + 1.0 &&
                location.Y >= y &&
                location.Y < y + 1.0 &&
                location.Z == z;
        }

        //TODO: test this
        public bool IsOpenSpace(XYZ xyz)
        {
            return true; // Exists(xyz) && this[xyz].IsOpenSpace;
        }

        public bool Exists(XYZ xyz)
        {
            return true;  //IsInZBounds(xyz) && _layers[xyz.Z].Exists(xyz);
        }

        public void ShowLayer(int layer)
        {
            //_world.HideEverything();
            //_layers[layer].ForEach(space => _world.Show(space.Obj, new XYZ(space.Location, layer)));
        }
    }
}
