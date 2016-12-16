using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurityConsultantCore.Domain;
using SecurityConsultantCore.Domain.Basic;
using SecurityConsultantCore.MapGeneration;
using SecurityConsultantCore.Test.EngineMocks;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

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
        public void FacilityMap__()
        {

        }

        //[TestMethod]
        //public void FacilityMap_Valuables_AreCorrect()
        //{
        //    var value1 = new ValuableFacilityObject { Type = "Painting" };
        //    var container = new ValuablesContainer { Type = "Table" };
        //    var value2 = new Valuable { Type = "Diamond" };
        //    container.Put(value2);
        //    _map[0].Put(new XYOrientation(5.0, 6.0), value1);
        //    _map[0].Put(new XYOrientation(7.0, 8.0), container);

        //    var valuables = _map.SpatialValuables.ToList();

        //    Assert.AreEqual(2, valuables.Count);
        //    Assert.IsTrue(valuables.Any(x => x.Obj.Type.Equals("Painting")));
        //    Assert.IsTrue(valuables.Any(x => x.Obj.Type.Equals("Diamond")));
        //}

        //[TestMethod]
        //public void FacilityMap_Portals_AreCorrect()
        //{
        //    var value1 = new FacilityPortal { Type = "Door" };
        //    var value2 = new FacilityPortal { Type = "Window" };
        //    _map[0].Put(new XYOrientation(5.0, 6.0), value1);
        //    _map[0].Put(new XYOrientation(7.0, 8.0), value2);

        //    var portals = _map.Portals.ToList();

        //    Assert.AreEqual(2, portals.Count());
        //    Assert.AreEqual(value1, portals.First(x => x.Location.Equals(new XYZ(5, 6, 0))).Obj);
        //    Assert.AreEqual(value2, portals.First(x => x.Location.Equals(new XYZ(7, 8, 0))).Obj);
        //}

        //[TestMethod]
        //public void FacilityMap_RemoveValuable_ValuableNoLongerInSpace()
        //{
        //    _map.Add(_sampleLayer);
        //    var jewels = new ValuableFacilityObject { Type = "Jewels", ObjectLayer = ObjectLayer.LowerObject };
        //    _map[5, 5, 0].Put(jewels);

        //    _map.Remove(jewels);

        //    Assert.AreEqual(true, _map[5, 5, 0].IsEmpty);
        //}

        //[TestMethod]
        //public void FacilityMap_Exists_ReturnsFalse()
        //{
        //    _sampleLayer.Put(1, 1, _sampleSpace);

        //    _map.Add(_sampleLayer);

        //    Assert.IsFalse(_map.Exists(new XYZ(1, 1, 2)));
        //}

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

        //private FacilityObject CreateFloor()
        //{
        //    return new FacilityObject { ObjectLayer = ObjectLayer.Ground, Type = "Floor" };
        //}
    }
}
