using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurityConsultantCore.Domain;
using SecurityConsultantCore.Domain.Basic;
using SecurityConsultantCore.Interactors;

namespace SecurityConsultantCore.Test.Interactors
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class SecurityPlacementValidatorTests
    {
        private SecurityPlacementValidator _validator;

        [TestMethod]
        public void SecurityPlacementValidator_EmptyMap_CannotPlaceSecurityObject()
        {
            SetMap(CreateSampleMap(new FacilitySpace()));

            AssertCannotPlace(new SecurityObject());
        }

        [TestMethod]
        public void SecurityPlacementValidator_SecurityObjectHasNoSlot_CannotPlaceSecurityObject()
        {
            SetMap(CreateSampleMap(new FacilitySpace { Ground = CreateFloor() }));

            AssertCannotPlace(new SecurityObject());
        }

        [TestMethod]
        public void SecurityPlacementValidator_CanPlaceSecObjInEmptySlot_IsTrue()
        {
            SetMap(CreateSampleMap(new FacilitySpace { Ground = CreateFloor() }));

            AssertCanPlace(new SecurityObject { ObjectLayer = ObjectLayer.GroundPlaceable });
            AssertCanPlace(new SecurityObject { ObjectLayer = ObjectLayer.LowerPlaceable });
            AssertCanPlace(new SecurityObject { ObjectLayer = ObjectLayer.UpperPlaceable });
        }

        [TestMethod]
        public void SecurityPlacementValidator_CanPlaceGroundSecObjOnTakenSpot_IsFalse()
        {
            SetMap(CreateSampleMap(new FacilitySpace { Ground = CreateFloor(), GroundPlaceable = CreateObject("Thing") }));

            AssertCannotPlace(new SecurityObject { ObjectLayer = ObjectLayer.GroundPlaceable });
        }

        [TestMethod]
        public void SecurityPlacementValidator_CanPlaceLowerSecObjOnTakenSpot_IsFalse()
        {
            SetMap(CreateSampleMap(new FacilitySpace { Ground = CreateFloor(), LowerPlaceable = CreateObject("Thing") }));

            AssertCannotPlace(new SecurityObject { ObjectLayer = ObjectLayer.LowerPlaceable });
        }

        [TestMethod]
        public void SecurityPlacementValidator_CanPlaceUpperSecObjOnTakenSpot_IsFalse()
        {
            SetMap(CreateSampleMap(new FacilitySpace { Ground = CreateFloor(), UpperPlaceable = CreateObject("Thing") }));

            AssertCannotPlace(new SecurityObject { ObjectLayer = ObjectLayer.UpperPlaceable });
        }

        [TestMethod]
        public void SecurityPlacementValidator_CanPlaceWallAttachmentOnNonTarget_IsFalse()
        {
            SetMap(CreateSampleMap(new FacilitySpace { Ground = CreateFloor() }));

            AssertCannotPlace(new SecurityObject { ObjectLayer = ObjectLayer.UpperPlaceable, Traits = new [] {"Attachment:Wall"} });
        }

        [TestMethod]
        public void SecurityPlacementValidator_CanPlaceWallAttachmentOnTarget_IsTrue()
        {
            SetMap(CreateSampleMap(new FacilitySpace { Ground = CreateFloor(), LowerObject = CreateObject("Wall") }));

            AssertCanPlace(new SecurityObject { ObjectLayer = ObjectLayer.UpperPlaceable, Traits = new[] {"Attachment:Wall"}});
        }

        [TestMethod]
        public void SecurityPlacementValidator_CanPlaceWallAttachmentOnModifiedTarget_IsTrue()
        {
            SetMap(CreateSampleMap(new FacilitySpace { Ground = CreateFloor(), LowerObject = CreateObject("Wall-Left") }));

            AssertCanPlace(new SecurityObject { ObjectLayer = ObjectLayer.UpperPlaceable, Traits = new[] { "Attachment:Wall" } });
        }

        [TestMethod]
        public void SecurityPlacementValidator_CanPlaceValuablesAttachmentOnNonTarget_IsFalse()
        {
            SetMap(CreateSampleMap(new FacilitySpace { Ground = CreateFloor() }));

            AssertCannotPlace(new SecurityObject { ObjectLayer = ObjectLayer.UpperPlaceable, Traits = new[] { "Attachment:LocatedValuables" } });
        }

        [TestMethod]
        public void SecurityPlacementValidator_CanPlaceValuablesAttachmentOnTarget_IsTrue()
        {
            SetMap(CreateSampleMap(new FacilitySpace { Ground = CreateFloor(), LowerObject = CreateObject("LocatedValuables-StackOfMoney") }));

            AssertCanPlace(new SecurityObject { ObjectLayer = ObjectLayer.UpperPlaceable, Traits = new[] { "Attachment:LocatedValuables" } });
        }

        [TestMethod]
        public void SecurityPlacementValidator_CanPlaceOpenSpaceSecObjOnSpaceWithLowerObject_IsFalse()
        {
            SetMap(CreateSampleMap(new FacilitySpace { Ground = CreateFloor(), LowerObject = CreateObject("TableEnd-Down") }));

            AssertCannotPlace(new SecurityObject { ObjectLayer = ObjectLayer.LowerPlaceable, Traits = new[] { "OpenSpace" } });
        }

        [TestMethod]
        public void SecurityPlacementValidator_CanPlaceOpenSpaceSecObjOnOpenSpace_IsTrue()
        {
            SetMap(CreateSampleMap(new FacilitySpace { Ground = CreateFloor() }));

            AssertCanPlace(new SecurityObject { ObjectLayer = ObjectLayer.LowerPlaceable, Traits = new[] { "OpenSpace" } });
        }

        [TestMethod]
        public void SecurityPlacementValidator_CanPlaceOpenSpaceSecObjOnSpaceWithCeilingObj_IsTrue()
        {
            SetMap(CreateSampleMap(new FacilitySpace { Ground = CreateFloor(), Ceiling = CreateObject("CeilingFan") }));

            AssertCanPlace(new SecurityObject { ObjectLayer = ObjectLayer.LowerPlaceable, Traits = new[] { "OpenSpace" } });
        }

        [TestMethod]
        public void SecurityPlacementValidator_CannotPlaceOpenSpaceWithNoGround_IsFalse()
        {
            SetMap(CreateSampleMap(new FacilitySpace()));

            AssertCannotPlace(new SecurityObject { ObjectLayer = ObjectLayer.LowerPlaceable, Traits = new[] { "OpenSpace" } });
        }

        private void SetMap(FacilityMap map)
        {
            _validator = new SecurityPlacementValidator(map);
        }

        private static FacilityObject CreateObject(string type)
        {
            return new FacilityObject { Type = type };
        }

        private static FacilityObject CreateFloor()
        {
            return CreateObject("Floor");
        }

        private void AssertCanPlace(SecurityObject securityObject)
        {
            Assert.IsTrue(_validator.CanPlace(0, 0, 0, securityObject));
            Assert.IsTrue(_validator.CanPlace(new XYZ(0, 0, 0), securityObject));
        }

        private void AssertCannotPlace(SecurityObject securityObject)
        {
            Assert.IsFalse(_validator.CanPlace(0, 0, 0, securityObject));
            Assert.IsFalse(_validator.CanPlace(new XYZ(0, 0, 0), securityObject));
        }

        private FacilityMap CreateSampleMap(FacilitySpace facilitySpace)
        {
            var map = new FacilityMap();
            var layer = new FacilityLayer();
            layer.Put(0, 0, facilitySpace);
            map.Add(layer);
            return map;
        }
    }
}
