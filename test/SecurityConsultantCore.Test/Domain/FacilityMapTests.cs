using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurityConsultantCore.Domain;
using SecurityConsultantCore.Domain.Basic;
using SecurityConsultantCore.MapGeneration;
using SecurityConsultantCore.Spatial;
using SecurityConsultantCore.Test.EngineMocks;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System;
using SecurityConsultantCore.Common;

namespace SecurityConsultantCore.Test.Domain
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class FacilityMapTests
    {
        private FacilityMap _map;
        private InMemoryWorld _world;
        private StructureObject _sampleFloor;

        [TestInitialize]
        public void Init()
        {
            _world = new InMemoryWorld();
            _map = new FacilityMap(_world);
            _sampleFloor = new StructureObject { Type = "Floor" };
        }

        [TestMethod]
        public void FacilityMap_PutObjectOnMap_ObjectIsOnMap()
        {
            _map.Put(new XYZOrientation(0.0, 0.0, 0, Orientation.Default), _sampleFloor);

            var query = _map.ObjectsOnSpace(0.0, 0.0, 0);
            Assert.AreEqual(_sampleFloor, query.First().Obj);
        }

        [TestMethod]
        public void FacilityMap_ObjectsOnSpace_OnlyObjectsInSpaceReturned()
        {
            _map.Put(new XYZOrientation(0.0, 0.0, 0, Orientation.Default), _sampleFloor);
            _map.Put(new XYZOrientation(1.0, 1.0, 0, Orientation.Default), _sampleFloor);

            var query = _map.ObjectsOnSpace(0.0, 0.0, 0);

            Assert.AreEqual(1, query.Count());
            Assert.AreEqual(_sampleFloor, query.First().Obj);
        }

        [TestMethod]
        public void FacilityMap_OpenSpacesWithLShapedObject_CorrectSpacesOccupied()
        {
            var lObject = new FacilityObject { Volume = new Volume(new XY(0, 0), new XY(1, 0), new XY(0, 1)) };
            _map.Put(new XYZOrientation(1.0, 1.0, 0, Orientation.Default), lObject);

            AssertSpacesNotOpen(new XYZ(1.0, 1.0, 0), new XYZ(2.0, 1.0, 0), new XYZ(1.0, 2.0, 0));
            AssertSpacesOpen(new XYZ(0.0, 0.0, 0), new XYZ(0.0, 1.0, 0), new XYZ(0.0, 2.0, 0),
                new XYZ(1.0, 0.0, 0), new XYZ(2.0, 0.0, 0), new XYZ(2.0, 2.0, 0));
        }

        [TestMethod]
        public void FacilityMap_Valuables_AreCorrect()
        {
            var value1 = new ValuableFacilityObject { Type = "Painting" };
            var value2 = new ValuableFacilityObject { Type = "Diamond" };
            _map.Put(new XYZOrientation(0, 0, 0), value1);
            _map.Put(new XYZOrientation(0, 0, 0), value2);

            var valuables = _map.SpatialValuables.ToList();

            Assert.AreEqual(2, valuables.Count);
            Assert.IsTrue(valuables.Any(x => x.Obj.Type.Equals("Painting")));
            Assert.IsTrue(valuables.Any(x => x.Obj.Type.Equals("Diamond")));
        }

        [TestMethod]
        public void FacilityMap_Portals_AreCorrect()
        {
            var value1 = new FacilityPortal { Type = "Door" };
            var value2 = new FacilityPortal { Type = "Window" };
            _map.Put(new XYZOrientation(5.0, 6.0, 0), value1);
            _map.Put(new XYZOrientation(7.0, 8.0, 0), value2);

            var portals = _map.Portals.ToList();

            Assert.AreEqual(2, portals.Count());
            Assert.AreEqual(value1, portals.First(x => x.Location.Equals(new XYZ(5, 6, 0))).Obj);
            Assert.AreEqual(value2, portals.First(x => x.Location.Equals(new XYZ(7, 8, 0))).Obj);
        }

        [TestMethod]
        public void FacilityMap_RemoveValuable_ValuableNoLongerInSpace()
        {
            var jewels = new ValuableFacilityObject { Type = "Jewels" };
            _map.Put(new XYZOrientation(5, 5, 0), jewels);

            _map.Remove(jewels);

            Assert.IsFalse(_map.SpatialValuables.Any(x => x.Obj.Equals(jewels)));
        }

        [TestMethod]
        public void FacilityMap_RemoveOnlyVolumetricObject_IsOpenTrue()
        {
            var jewels = new ValuableFacilityObject { Type = "Jewels", Volume = new Volume(new XY(0, 0)) };
            _map.Put(new XYZOrientation(5, 5, 0), jewels);

            _map.Remove(jewels);

            Assert.IsTrue(_map.IsOpen(new XYZ(5, 5, 0)));
        }

        [TestMethod]
        public void FacilityMap_TwoVolumesSameLocationOneRemoved_IsOpenFalse()
        {
            var jewels = new ValuableFacilityObject { Type = "Jewels", Volume = new Volume(new XY(0, 0)) };
            var jewels2 = new ValuableFacilityObject { Type = "Jewels", Volume = new Volume(new XY(0, 0)) };

            _map.Put(new XYZOrientation(5, 5, 0), jewels);
            _map.Put(new XYZOrientation(5, 5, 0), jewels2);

            _map.Remove(jewels);

            Assert.IsFalse(_map.IsOpen(new XYZ(5, 5, 0)));
        }

        [TestMethod]
        public void FacilityMap_SmallOnLargeObjectLargeObjectRemoved_SmallObjectRemains()
        {
            var jewels = new ValuableFacilityObject { Type = "Jewels", Volume = new Volume(new XY(0, 0)) };
            var table = new ValuableFacilityObject { Type = "Table", Volume = new Volume(new XY(0, 0), new XY(1, 0)) };

            _map.Put(new XYZOrientation(5, 5, 0), jewels);
            _map.Put(new XYZOrientation(5, 5, 0), table);

            _map.Remove(table);

            Assert.AreEqual(1, _map.SpatialValuables.Count());
            Assert.IsFalse(_map.IsOpen(new XYZ(5, 5, 0)));
            Assert.IsTrue(_map.IsOpen(new XYZ(6, 5, 0)));
        }

        [TestMethod]
        [Ignore]
        // Currently working RIGHT HERE, have to have some notion of extents that expand as we add objects to the FacilityMap
        public void FacilityMap_SingleTileVolume_ExistsCorrect()
        {
            _map.Put(new XYZOrientation(1, 2, 3), new FacilityObject { Volume = new Volume(new XY(0, 0)) });   

            Assert.IsTrue(_map.Exists(new XYZ(1, 2, 3)));
            Assert.IsFalse(_map.Exists(new XYZ(0, 2, 3)));
            Assert.IsFalse(_map.Exists(new XYZ(1, 40000, 3)));
            Assert.IsFalse(_map.Exists(new XYZ(255, 2, 100)));
            Assert.IsFalse(_map.Exists(SpecialLocation.OffOfMap));
        }

        //[TestMethod]
        //public void FacilityMap_Exist_ReturnsTrue()
        //{
        //    _sampleLayer.Put(1, 1, _sampleSpace);

        //    _map.Add(_sampleLayer);

        //    Assert.IsFalse(_map.Exists(new XYZ(1, 1, 1)));
        //}

        //[TestMethod]
        //public void FacilityMap_EmptyMapInstruction_EmptyMapReturned()
        //{
        //    var map = new FacilityMap(_world, CreateInstruction());

        //    Assert.AreEqual(0, map.LayerCount);
        //}

        //[TestMethod]
        //public void FacilityMap_SinglePortalMapInstruction_LayersAndPortalsCorrect()
        //{
        //    var map = new FacilityMap(_world, CreateInstruction("Layer:Size=1,1", "Portal-Door:0,0,0;End1=(OffMap);End2=(OffMap)"));

        //    Assert.AreEqual(1, map.LayerCount);
        //    Assert.AreEqual(1, map.Portals.Count());
        //}

        //[TestMethod]
        //public void FacilityMap_WhenShowingLayer_AllObjectsInWorld()
        //{
        //    var floor = CreateFloor();
        //    _sampleSpace.Put(floor);
        //    _sampleLayer.Put(1, 1, _sampleSpace);
        //    _map.Add(_sampleLayer);

        //    _map.ShowLayer(0);

        //    Assert.AreEqual(floor, _world.ObjectAt(new XYZ(1, 1, 0), ObjectLayer.Ground));
        //}

        //[TestMethod]
        //public void FacilityMap_WhenShowingLayerTwoObjects_AllObjectsInWorld()
        //{
        //    var floor = CreateFloor();
        //    var vase = new FacilityObject { ObjectLayer = ObjectLayer.LowerObject, Type = "Vase" };
        //    _sampleSpace.Put(floor);
        //    _sampleSpace.Put(vase);
        //    _sampleLayer.Put(1, 1, _sampleSpace);
        //    _map.Add(_sampleLayer);

        //    _map.ShowLayer(0);

        //    Assert.AreEqual(floor, _world.ObjectAt(new XYZ(1, 1, 0), ObjectLayer.Ground));
        //    Assert.AreEqual(vase, _world.ObjectAt(new XYZ(1, 1, 0), ObjectLayer.LowerObject));
        //}

        //[TestMethod]
        //public void FacilityMap_WhenShowingSecondLayer_FirstLayerNotVisible()
        //{
        //    var floor = CreateFloor();
        //    _sampleSpace.Put(floor);
        //    _sampleLayer.Put(1, 1, _sampleSpace);
        //    _map.Add(_sampleLayer);
        //    _map.Add(new FacilityLayer());

        //    _map.ShowLayer(0);
        //    _map.ShowLayer(1);

        //    Assert.AreEqual(new FacilityObject(), _world.ObjectAt(new XYZ(1, 1, 0), ObjectLayer.Ground));
        //}

        private MapInstruction CreateInstruction(params string[] args)
        {
            return MapInstruction.FromStrings(args.ToList());
        }

        private void AssertSpacesNotOpen(params XYZ[] xyzs)
        {
            xyzs.ForEach(xyz => Assert.IsFalse(_map.IsOpen(xyz), xyz.ToString()));
        }

        private void AssertSpacesOpen(params XYZ[] xyzs)
        {
            xyzs.ForEach(xyz => Assert.IsTrue(_map.IsOpen(xyz), xyz.ToString()));
        }

        //private FacilityObject CreateFloor()
        //{
        //    return new FacilityObject { ObjectLayer = ObjectLayer.Ground, Type = "Floor" };
        //}
    }
}
