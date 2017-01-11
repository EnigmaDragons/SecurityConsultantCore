using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurityConsultantCore.Domain;
using SecurityConsultantCore.Domain.Basic;
using SecurityConsultantCore.PlayerCommands;
using SecurityConsultantCore.Test.EngineMocks;
using SecurityConsultantCore.Test._TestDoubles;

namespace SecurityConsultantCore.Test.PlayerCommands
{
    [TestClass]
    public class EraseCommandTests
    {
        private FacilityMap _map;

        [TestInitialize]
        public void Init()
        {
            _map = new FacilityMap(new InMemoryWorld());
            _map.Add(new FacilityLayer(1, 1));
        }

        [TestMethod]
        public void EraseCommand_EraseSecurityObjectAtLocation_ObjectGone()
        {
            _map[0, 0, 0].Put(new FakeSecurityObject { ObjectLayer = ObjectLayer.LowerObject });
            var command = new EraseCommand(_map, new XYZ(0, 0, 0));

            command.Go();

            Assert.AreEqual(_map[0, 0, 0].LowerObject.Type, "None");
        }

        [TestMethod]
        public void EraseCommand_EraseSecurityObject_ValuableNotRemoved()
        {
            _map[0, 0, 0].Put(new FakeSecurityObject { Type = "Camera", ObjectLayer = ObjectLayer.LowerObject });
            _map[0, 0, 0].Put(new ValuableFacilityObject { Type = "Diamonds", ObjectLayer = ObjectLayer.Ground });
            var command = new EraseCommand(_map, new XYZ(0, 0, 0));

            command.Go();

            Assert.AreEqual(_map[0, 0, 0].Ground.Type, "Diamonds");
        }
    }
}
