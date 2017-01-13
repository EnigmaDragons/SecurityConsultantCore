using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurityConsultantCore.Domain;
using SecurityConsultantCore.Domain.Basic;
using SecurityConsultantCore.Player;
using SecurityConsultantCore.Test.Engine;
using SecurityConsultantCore.Test._TestDoubles;

namespace SecurityConsultantCore.Test.Player
{
    [TestClass]
    public class BuildCommandTests
    {
        private FacilityMap _map;

        [TestInitialize]
        public void Init()
        {
            _map = new FacilityMap(new InMemoryWorld());
            _map.Add(new FacilityLayer(3, 3));
        }

        [TestMethod]
        public void BuildCommand_Build_ObjectPlaced()
        {
            var securityObj = CreateLowerSecurityObject();
            var command = new BuildCommand(_map, new XYZ(0, 0, 0), securityObj);

            command.Go();

            Assert.AreEqual(_map[0, 0, 0].LowerPlaceable, securityObj);
        }

        [TestMethod]
        public void BuildCommand_BuildOnOccupiedLocation_ObjectNotPlaced()
        {
            var obj1 = CreateLowerSecurityObject("Lasers");
            _map[0, 0, 0].Put(obj1);
            var obj2 = CreateLowerSecurityObject("CannonBall");
            var command = new BuildCommand(_map, new XYZ(0, 0, 0), obj2);

            command.Go();

            Assert.AreEqual(_map[0, 0, 0].LowerPlaceable, obj1);
            Assert.AreNotEqual(_map[0, 0, 0].LowerPlaceable, obj2);
        }

        [TestMethod]
        public void BuildCommand_PlaceOffMap_NoExceptionsThrown()
        {
            var command = new BuildCommand(_map, SpecialLocation.OffOfMap, CreateLowerSecurityObject());

            command.Go();
        }

        [TestMethod]
        public void BuildCommand_TryPlacingLargeSecurityObjOnWall_ObjectNotPlaced()
        {
            _map[0, 0, 0].Put(new FacilityObject { ObjectLayer = ObjectLayer.LowerObject, Type = "Wall" });
            var secObj = CreateLowerSecurityObject();
            var command = new BuildCommand(_map, new XYZ(0, 0, 0), secObj);

            command.Go();

            Assert.AreNotEqual(_map[0, 0, 0].LowerPlaceable, secObj);
        }

        private FakeSecurityObject CreateLowerSecurityObject()
        {
            return CreateLowerSecurityObject("Security");
        }

        private FakeSecurityObject CreateLowerSecurityObject(string type)
        {
            return new FakeSecurityObject { ObjectLayer = ObjectLayer.LowerPlaceable, Type = type };
        }
    }
}
