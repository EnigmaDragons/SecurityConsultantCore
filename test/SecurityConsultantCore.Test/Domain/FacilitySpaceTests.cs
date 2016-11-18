using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurityConsultantCore.Domain;
using SecurityConsultantCore.Domain.Basic;
using SecurityConsultantCore.Common;

namespace SecurityConsultantCore.Test.Domain
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class FacilitySpaceTests
    {
        [TestMethod]
        public void FacilitySpace_CreateEmptySpace_HasFloor()
        {
            var space = new FacilitySpace();

            AssertSpaceContentsAre(space);
        }
        
        [TestMethod]
        public void FacilitySpace_SetupUpDetailedSpace_SpaceCorrect()
        {
            var space = new FacilitySpace
            {
                Ground = CreateFacilityObject("Floor"),
                LowerObject = CreateFacilityObject("TableCenter"),
                UpperObject = CreateFacilityObject("TennisRacket"),
                Ceiling = CreateFacilityObject("CeilingFan")
            };

            AssertSpaceContentsAre(space, "Floor", "TableCenter", "TennisRacket", "CeilingFan");
        }

        [TestMethod]
        public void FacilitySpace_Valuables_ReturnedCorrectly()
        {
            var space = new FacilitySpace
            {
                Ground = CreateFacilityObject("Floor"),
                LowerObject = CreateValuable("DisplayRack"),
                UpperObject = CreateValuable("TennisRacket"),
            };

            var valuables = space.Valuables.ToList();

            Assert.AreEqual(2, valuables.Count());
            Assert.IsTrue(valuables.Any(x => x.Type.Equals("DisplayRack")));
            Assert.IsTrue(valuables.Any(x => x.Type.Equals("TennisRacket")));
        }

        [TestMethod]
        public void FacilitySpace_ValuableInContainer_ReturnsCorrectly()
        {
            var space = new FacilitySpace();
            var container = new ValuablesContainer { ObjectLayer = ObjectLayer.LowerObject };
            container.Put(new Valuable { Type = "Diamond" });
            space.Put(container);

            var valuables = space.Valuables.ToList();

            Assert.AreEqual(1, valuables.Count());
            Assert.IsTrue(valuables.Any(x => x.Type.Equals("Diamond")));
        }

        [TestMethod]
        public void FacilitySpace_Portals_ReturnedCorrectly()
        {
            var space = new FacilitySpace
            {
                Ground = CreatePortal("Manhole"),
                UpperObject = CreatePortal("DoubleDoorLeftSide"),
                Ceiling = CreatePortal("VentilationShaft")
            };

            var portals = space.Portals.ToList();

            Assert.AreEqual(3, portals.Count());
            Assert.IsTrue(portals.Any(x => x.Type.Equals("Manhole")));
            Assert.IsTrue(portals.Any(x => x.Type.Equals("Manhole")));
            Assert.IsTrue(portals.Any(x => x.Type.Equals("DoubleDoorLeftSide")));
        }

        [TestMethod]
        public void FacilitySpace_PutObject_ObjectPlacedCorrectly()
        {
            var space = new FacilitySpace();
            var street = new FacilityObject {Type = "Street", ObjectLayer = ObjectLayer.Ground};

            space.Put(street);

            Assert.AreEqual(street, space.Ground);
        }

        [ExpectedException(typeof(InvalidOperationException))]
        [TestMethod]
        public void FacilitySpace_PutUnknownLayerObject_ThrowsException()
        {
            var space = new FacilitySpace();
            var street = new FacilityObject { Type = "Garbage", ObjectLayer = ObjectLayer.Unknown };

            space.Put(street);
        }

        [TestMethod]
        public void FacilitySpace_ReplaceObjectInLayer_LaterObjectExistsInSpace()
        {
            var space = new FacilitySpace();
            var street = new FacilityObject { Type = "Street", ObjectLayer = ObjectLayer.Ground };
            var floor = new FacilityObject { Type = "Floor", ObjectLayer = ObjectLayer.Ground };

            space.Put(street);
            space.Put(floor);

            Assert.AreEqual(floor, space.Ground);
        }

        [TestMethod]
        public void FacilitySpace_Placeables_ReturnsCorrectly()
        {
            var space = new FacilitySpace
            {
                Ground = CreateFacilityObject("Floor"),
                GroundPlaceable = CreateSecurityObject("PressureMat"),
                LowerPlaceable = CreateSecurityObject("Turret"),
                UpperObject = CreateSecurityObject("SecurityCamera"),
            };

            var placeables = space.Placeables.ToList();

            Assert.AreEqual(3, placeables.Count());
            Assert.IsTrue(placeables.Any(x => x.Type.Equals("PressureMat")));
            Assert.IsTrue(placeables.Any(x => x.Type.Equals("Turret")));
            Assert.IsTrue(placeables.Any(x => x.Type.Equals("SecurityCamera")));
        }

        [TestMethod]
        public void FacilitySpace_RemoveByFacilityObject_ObjectRemoved()
        {
            var space = new FacilitySpace { Ground = new FacilityObject { Type = "Floor", ObjectLayer = ObjectLayer.Ground } };

            space.Remove(space.Ground);

            Assert.AreEqual("None", space.Ground.Type);
        }

        [TestMethod]
        public void FacilitySpace_Index_CorrectObjectReturned()
        {
            var obj = new FacilityObject {Type = "Floor", ObjectLayer = ObjectLayer.Ground};
            var space = new FacilitySpace { Ground = obj };

            var result = space[ObjectLayer.Ground];

            Assert.AreEqual(obj, result);
        }

        [TestMethod]
        public void FacilitySpace_RemoveByLayer_ObjectRemoved()
        {
            var space = new FacilitySpace { Ground = new FacilityObject { Type = "Floor", ObjectLayer = ObjectLayer.Ground } };

            space.Remove(ObjectLayer.Ground);

            Assert.AreEqual("None", space.Ground.Type);
        }

        [TestMethod]
        public void FacilitySpace_ObjectPlaced_ObserverNotified()
        {
            var space = new FacilitySpace();
            var obs = new SimpleObserver<FacilitySpace>();
            var obj = new FacilityObject { Type = "Floor", ObjectLayer = ObjectLayer.Ground };
            space.Subscribe(obs);

            space.Put(obj);

            Assert.AreEqual(space, obs.LastUpdate);
        }

        [TestMethod]
        public void FacilitySpace_RemoveFacilityObject_ObserverNotified()
        {
            var space = new FacilitySpace();
            var obs = new SimpleObserver<FacilitySpace>();
            var obj = new FacilityObject { Type = "Floor", ObjectLayer = ObjectLayer.Ground };
            space.Put(obj);
            space.Subscribe(obs);

            space.Remove(obj);

            Assert.AreEqual(space, obs.LastUpdate);
        }

        [TestMethod]
        public void FacilitySpace_RemoveObjectLayer_ObserverNotified()
        {
            var space = new FacilitySpace();
            var obs = new SimpleObserver<FacilitySpace>();
            var obj = new FacilityObject { Type = "Floor", ObjectLayer = ObjectLayer.Ground };
            space.Put(obj);
            space.Subscribe(obs);

            space.Remove(ObjectLayer.Ground);

            Assert.AreEqual(space, obs.LastUpdate);
        }

        [TestMethod]
        public void FacilitySpace_RemoveValuable_ObserverNotified()
        {
            var space = new FacilitySpace();
            var obs = new SimpleObserver<FacilitySpace>();
            var valuable = new ValuableFacilityObject { Type = "Vase", ObjectLayer = ObjectLayer.LowerObject };
            space.Put(valuable);
            space.Subscribe(obs);

            space.Remove((IValuable)valuable);

            Assert.AreEqual(space, obs.LastUpdate);
        }

        [TestMethod]
        public void FacilitySpace_NoValuableRemoved_ObserverNotNotified()
        {
            var space = new FacilitySpace();
            var obs = new SimpleObserver<FacilitySpace>();
            var obj = new FacilityObject { Type = "Floor", ObjectLayer = ObjectLayer.Ground };
            space.Put(obj);
            space.Subscribe(obs);

            space.Remove(new FacilityObject { Type = "Door" });

            Assert.AreEqual(0, obs.UpdateCount);
        }

        private FacilityObject CreatePortal(string portalType)
        {
            return new FacilityPortal { Type = portalType };
        }

        private FacilityObject CreateValuable(string valuableType)
        {
            return new ValuableFacilityObject { Type = valuableType };
        }

        private FacilityObject CreateFacilityObject(string type)
        {
            return new FacilityObject { Type = type };
        }

        private FacilityObject CreateSecurityObject(string securityType)
        {
            return new SecurityObject { Type = securityType };
        }
        
        private void AssertSpaceContentsAre(FacilitySpace space, string ground = "None", string lowerObj = "None", string upperObj = "None", 
            string ceiling = "None", string lowerPlc = "None", string upperPlc = "None", string groundPlc = "None")
        {
            Assert.AreEqual(ground, space.Ground.Type);
            Assert.AreEqual(lowerObj, space.LowerObject.Type);
            Assert.AreEqual(upperObj, space.UpperObject.Type);
            Assert.AreEqual(ceiling, space.Ceiling.Type);
            Assert.AreEqual(lowerPlc, space.LowerPlaceable.Type);
            Assert.AreEqual(upperPlc, space.UpperPlaceable.Type);
            Assert.AreEqual(groundPlc, space.GroundPlaceable.Type);
        }
    }
}
