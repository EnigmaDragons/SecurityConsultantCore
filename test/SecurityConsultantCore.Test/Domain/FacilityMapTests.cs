﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurityConsultantCore.Domain;
using SecurityConsultantCore.Domain.Basic;
using SecurityConsultantCore.Spatial;
using SecurityConsultantCore.Test.EngineMocks;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using SecurityConsultantCore.Common;
using System.Collections.Generic;

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
            _map.Put(_sampleFloor, new XYZOrientation(0.0, 0.0, 0, Orientation.Default));

            var query = _map.WhatIsAt(new XYZ(0.0, 0.0, 0));
            Assert.AreEqual(_sampleFloor, query.First().Obj);
        }

        [TestMethod]
        public void FacilityMap_ObjectsOnSpace_OnlyObjectsInSpaceReturned()
        {
            _map.Put(_sampleFloor, new XYZOrientation(0.0, 0.0, 0, Orientation.Default));
            _map.Put(_sampleFloor, new XYZOrientation(1.0, 1.0, 0, Orientation.Default));

            var query = _map.WhatIsAt(new XYZ(0.0, 0.0, 0));

            Assert.AreEqual(1, query.Count());
            Assert.AreEqual(_sampleFloor, query.First().Obj);
        }

        [TestMethod]
        public void FacilityMap_OpenSpacesWithLShapedObject_CorrectSpacesOccupied()
        {
            var lObject = new FacilityObject { Volume = new Volume(new XYVolume(0, 0), new XYVolume(1, 0), new XYVolume(0, 1)) };
            _map.Put(lObject, new XYZOrientation(1.0, 1.0, 0, Orientation.Default));

            AssertSpacesNotOpen(new XYZ(1.0, 1.0, 0), new XYZ(2.0, 1.0, 0), new XYZ(1.0, 2.0, 0));
            AssertSpacesOpen(new XYZ(0.0, 0.0, 0), new XYZ(0.0, 1.0, 0), new XYZ(0.0, 2.0, 0),
                new XYZ(1.0, 0.0, 0), new XYZ(2.0, 0.0, 0), new XYZ(2.0, 2.0, 0));
        }

        [TestMethod]
        public void FacilityMap_Valuables_AreCorrect()
        {
            var value1 = new ValuableFacilityObject { Type = "Painting" };
            var value2 = new ValuableFacilityObject { Type = "Diamond" };
            _map.Put(value1, new XYZOrientation(0, 0, 0));
            _map.Put(value2, new XYZOrientation(0, 0, 0));

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
            _map.Put(value1, new XYZOrientation(5.0, 6.0, 0));
            _map.Put(value2, new XYZOrientation(7.0, 8.0, 0));

            var portals = _map.Portals.ToList();

            Assert.AreEqual(2, portals.Count());
            Assert.AreEqual(value1, portals.First(x => x.Location.Equals(new XYZ(5, 6, 0))).Obj);
            Assert.AreEqual(value2, portals.First(x => x.Location.Equals(new XYZ(7, 8, 0))).Obj);
        }

        [TestMethod]
        public void FacilityMap_RemoveValuable_ValuableNoLongerInSpace()
        {
            var jewels = new ValuableFacilityObject { Type = "Jewels" };
            _map.Put(jewels, new XYZOrientation(5, 5, 0));

            _map.Remove(jewels);

            Assert.IsFalse(_map.SpatialValuables.Any(x => x.Obj.Equals(jewels)));
        }

        [TestMethod]
        public void FacilityMap_RemoveOnlyVolumetricObject_IsOpenTrue()
        {
            var jewels = new ValuableFacilityObject { Type = "Jewels", Volume = new Volume(new XYVolume(0, 0)) };
            _map.Put(jewels, new XYZOrientation(5, 5, 0));

            _map.Remove(jewels);

            Assert.IsTrue(_map.IsOpen(new XYZ(5, 5, 0)));
        }

        [TestMethod]
        public void FacilityMap_TwoVolumesSameLocationOneRemoved_IsOpenFalse()
        {
            var jewels = new ValuableFacilityObject { Type = "Jewels", Volume = new Volume(new XYVolume(0, 0)) };
            var jewels2 = new ValuableFacilityObject { Type = "Jewels", Volume = new Volume(new XYVolume(0, 0)) };

            _map.Put(jewels, new XYZOrientation(5, 5, 0));
            _map.Put(jewels2, new XYZOrientation(5, 5, 0));

            _map.Remove(jewels);

            Assert.IsFalse(_map.IsOpen(new XYZ(5, 5, 0)));
        }

        [TestMethod]
        public void FacilityMap_SmallOnLargeObjectLargeObjectRemoved_SmallObjectRemains()
        {
            var jewels = new ValuableFacilityObject { Type = "Jewels", Volume = new Volume(new XYVolume(0, 0)) };
            var table = new ValuableFacilityObject { Type = "Table", Volume = new Volume(new XYVolume(0, 0), new XYVolume(1, 0)) };

            _map.Put(jewels, new XYZOrientation(5, 5, 0));
            _map.Put(table, new XYZOrientation(5, 5, 0));

            _map.Remove(table);

            Assert.AreEqual(1, _map.SpatialValuables.Count());
            Assert.IsFalse(_map.IsOpen(new XYZ(5, 5, 0)));
            Assert.IsTrue(_map.IsOpen(new XYZ(6, 5, 0)));
        }

        [TestMethod]
        public void FacilityMap_SingleTileVolume_ExistsCorrect()
        {
            _map.Put(new FacilityObject { Volume = new Volume(new XYVolume(0, 0)) }, new XYZOrientation(1, 2, 3));   

            Assert.IsTrue(_map.Exists(new XYZ(1, 2, 3)));
            Assert.IsFalse(_map.Exists(new XYZ(0, 2, 3)));
            Assert.IsFalse(_map.Exists(new XYZ(1, 40000, 3)));
            Assert.IsFalse(_map.Exists(new XYZ(255, 2, 100)));
            Assert.IsFalse(_map.Exists(SpecialLocation.OffOfMap));
        }

        [TestMethod]
        public void FacilityMap_MultiTileVolume_ExistsCorrect()
        {
            _map.Put(new FacilityObject { Volume = new Volume(new XYVolume(0, 0), new XYVolume(0, 1)) }, new XYZOrientation(1, 2, 3));

            Assert.IsTrue(_map.Exists(new XYZ(1, 2, 3)));
            Assert.IsTrue(_map.Exists(new XYZ(1, 3, 3)));
            Assert.IsFalse(_map.Exists(new XYZ(1, 1, 3)));
        }

        [TestMethod]
        public void FacilityMap_PlaceHalfWayOnSpace_IsNotOnSeperateSpace()
        {
            _map.Put(new FacilityObject { Volume = new Volume(new XYVolume(0, 0), new XYVolume(0, 1)) }, new XYZOrientation(0.6, 0.6, 0));

            Assert.AreEqual(0, _map.WhatIsAt(new XYZ(1, 1, 0)).Count());
        }

        [TestMethod]
        public void FacilityMap_FloorOnSpace_SpaceIsOpen()
        {
            _map.Put(CreateFloor(), new XYZOrientation(0, 0, 0));
            
            Assert.AreEqual(true, _map.IsOpen(new XYZ(0, 0, 0)));
        }

        [TestMethod]
        public void FacilityMap_WhenShowingLayer_AllObjectsInWorld()
        {
            var floor = CreateFloor();
            _map.Put(floor, new XYZOrientation(1, 1, 0));

            _map.ShowLayer(0);

            Assert.IsTrue(_world.IsObjectAt(floor, new XYZ(1, 1, 0)));
        }

        [TestMethod]
        public void FacilityMap_WhenShowingLayerTwoObjects_AllObjectsInWorld()
        {
            var floor = CreateFloor();
            var vase = new FacilityObject { Type = "Vase" };
            _map.Put(floor, new XYZOrientation(1, 1, 0));
            _map.Put(vase, new XYZOrientation(1, 1, 0));

            _map.ShowLayer(0);

            Assert.IsTrue(_world.IsObjectAt(floor, new XYZ(1, 1, 0)));
            Assert.IsTrue(_world.IsObjectAt(vase, new XYZ(1, 1, 0)));
        }

        [TestMethod]
        public void FacilityMap_WhenShowingSecondLayer_FirstLayerNotVisible()
        {
            var floor = CreateFloor();
            var vase = new FacilityObject { Type = "Vase" };
            _map.Put(floor, new XYZOrientation(1, 1, 0));
            _map.Put(vase, new XYZOrientation(1, 1, 1));

            _map.ShowLayer(0);
            _map.ShowLayer(1);

            Assert.IsTrue(_world.IsObjectAt(vase, new XYZ(1, 1, 1)));
        }

        [TestMethod]
        public void FacilityMap_Floors_AreCorrect()
        {
            var value1 = new FacilityPortal { Type = "Floor" };
            var value2 = new FacilityPortal { Type = "Floor" };
            _map.Put(value1, new XYZOrientation(5.0, 6.0, 0));
            _map.Put(value2, new XYZOrientation(7.0, 8.0, 0));

            var floors = _map.Portals.ToList();

            Assert.AreEqual(2, floors.Count());
            Assert.AreEqual(value1, floors.First(x => x.Location.Equals(new XYZ(5, 6, 0))).Obj);
            Assert.AreEqual(value2, floors.First(x => x.Location.Equals(new XYZ(7, 8, 0))).Obj);
        }

        private void AssertSpacesNotOpen(params XYZ[] xyzs)
        {
            xyzs.ForEach(xyz => Assert.IsFalse(_map.IsOpen(xyz), xyz.ToString()));
        }

        private void AssertSpacesOpen(params XYZ[] xyzs)
        {
            xyzs.ForEach(xyz => Assert.IsTrue(_map.IsOpen(xyz), xyz.ToString()));
        }

        private FacilityObject CreateFloor()
        {
            return new StructureObject { Type = "Floor" };
        }
    }
}
