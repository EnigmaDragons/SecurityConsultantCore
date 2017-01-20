﻿using SecurityConsultantCore.Common;
using SecurityConsultantCore.Domain.Basic;
using SecurityConsultantCore.EngineInterfaces;
using SecurityConsultantCore.FacilityObjects;
using SecurityConsultantCore.OOMath;
using System.Collections.Generic;
using System.Linq;
using System;

namespace SecurityConsultantCore.Domain
{
    public class FacilityMap
    {
        private readonly List<XYZOriented<FacilityObject>> _objects = new List<XYZOriented<FacilityObject>>();
        private readonly Dictionary<XYZ, int> _occupiedSpaces = new Dictionary<XYZ, int>();
        private readonly IWorld _world;

        public FacilityMap(IWorld world)
        {
            _world = world;
        }

        public void Put(FacilityObject obj, XYZOrientation xyzo)
        {
            _objects.Add(new XYZOriented<FacilityObject>(xyzo, obj));
            obj.Volume.GetOccupiedTiles(xyzo).ForEach(x => UpdateOccupiedSpaces(x, 1));
        }

        public IEnumerable<XYZOriented<FacilityObject>> WhatIsAt(XYZ xyz)
        {
            return _objects.Where(o => o.Location.Equals(xyz));
        }

        public IEnumerable<SpatialValuable> SpatialValuables => 
            _objects.Where(x => x.Obj is IValuable)
            .Select(x => new SpatialValuable(x, (IValuable)x.Obj));

        public IEnumerable<XYZLocation<FacilityPortal>> Portals =>
            _objects.Where(x => x.Obj is FacilityPortal)
            .Select(x => new XYZLocation<FacilityPortal>(x, (FacilityPortal)x.Obj));

        public IEnumerable<XYZOriented<FacilityObject>> Floors => _objects.Where(x => IsFloor(x.Obj));

        private bool IsFloor(FacilityObject obj)
        {
            return obj.Type.Equals(FacilityObjectNames.Floor);
        }

        public void Remove(IValuable valuable)
        {
            var found = _objects.FirstOrDefault(xyzo => xyzo.Obj.Equals(valuable));
            _objects.RemoveAll(xyzo => xyzo.Obj.Equals(valuable));
            if (found != null)
                found.Obj.Volume.GetOccupiedTiles(found).ForEach(x => UpdateOccupiedSpaces(x, -1));
        }

        public bool IsOpen(XYZ xyz)
        {
            return GetVolumetricObjectCount(xyz) == 0;
        }

        public bool Exists(Number x, Number y, Number z)
        {
            return Exists(new XYZ(x, y, z));
        }

        public bool Exists(XYZ xyz)
        {
            return _occupiedSpaces.ContainsKey(xyz);
        }

        public void ShowLayer(int layer)
        {
            _world.HideEverything();
            _objects.Where(o => o.Z.Equals(layer))
                .ForEach(o => _world.Show(o));
        }

        private void UpdateOccupiedSpaces(XYZ xyz, int modifier)
        {
            SetVolumetricObjectCount(xyz, GetVolumetricObjectCount(xyz) + modifier);
        }

        private void SetVolumetricObjectCount(XYZ xyz, int numObjs)
        {
            _occupiedSpaces[xyz] = numObjs;
        }

        private int GetVolumetricObjectCount(XYZ xyz)
        {
            if (!_occupiedSpaces.ContainsKey(xyz))
                _occupiedSpaces[xyz] = 0;
            return _occupiedSpaces[xyz];
        }
    }
}
