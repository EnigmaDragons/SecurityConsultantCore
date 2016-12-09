﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurityConsultantCore.Domain;
using SecurityConsultantCore.Domain.Basic;
using SecurityConsultantCore.PlayerCommands;
using SecurityConsultantCore.Test.EngineMocks;
using SecurityConsultantCore.Test._TestDoubles;

namespace SecurityConsultantCore.Test.PlayerCommands
{
    [TestClass]
    public class PlacementValidationTests
    {
        [TestMethod]
        public void PlacementValidation_PlaceOffMap_Invalid()
        {
            var validation = new PlacementValidation(new FacilityMap(new InMemoryWorld()), new FakeSecurityObject(), SpecialLocation.OffOfMap);

            var isValid = validation.Check();

            Assert.AreEqual(false, isValid);
        }

        [TestMethod]
        public void PlacementValidation_ValidPlacement_Valid()
        {
            var validation = new PlacementValidation(CreateMap(), new FakeSecurityObject { ObjectLayer = ObjectLayer.LowerObject }, new XYZ(0, 0, 0));

            var isValid = validation.Check();

            Assert.AreEqual(true, isValid);
        }

        [TestMethod]
        public void PlacementValidation_TryPlacingLargeSecurityObjOnWall_Invalid()
        {
            var map = CreateMap();
            map[0, 0, 0].Put(new FacilityObject { ObjectLayer = ObjectLayer.LowerObject, Type = "Wall" });
            var validation = new PlacementValidation(map, new FakeSecurityObject { ObjectLayer = ObjectLayer.LowerObject }, new XYZ(0, 0, 0));

            var isValid = validation.Check();

            Assert.AreEqual(false, isValid);
        }

        //TODO: wait til maps are redone to implement other criteria

        private FacilityMap CreateMap()
        {
            var map = new FacilityMap(new InMemoryWorld());
            map.Add(new FacilityLayer(1, 1));
            return map;
        }
    }
}