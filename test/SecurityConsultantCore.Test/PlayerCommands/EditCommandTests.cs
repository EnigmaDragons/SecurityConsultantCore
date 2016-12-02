using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurityConsultantCore.Domain;
using SecurityConsultantCore.Domain.Basic;
using SecurityConsultantCore.PlayerCommands;
using SecurityConsultantCore.Test.EngineMocks;

namespace SecurityConsultantCore.Test.PlayerCommands
{
    [TestClass]
    public class EditCommandTests : SecurityObjectBase, IEngineer
    {
        private FacilityMap _map;
        private FacilityLayer _layer;

        private IEngineer _player;
        private IEngineer _engineer;

        [TestInitialize]
        public void Init()
        {
            _player = this;
            ObjectLayer = ObjectLayer.GroundPlaceable;
            _map = new FacilityMap(new InMemoryWorld());
            _layer = new FacilityLayer(2, 2);
            _map.Add(_layer);
        }

        [TestMethod]
        public void EditCommand_GoWithNoObjectOnSpace_NoObjectConsultsWithNoPlayer()
        {
            var command = new EditCommand(_map, new XYZ(1, 1, 0), this);

            command.Go();

            Assert.AreEqual(null, _engineer);
        }

        [TestMethod]
        public void EditCommand_PlayerEditsASecurityObject_ObjectConsultsWithPlayer()
        {
            _layer[1, 1].Put(this);
            var command = new EditCommand(_map, new XYZ(1, 1, 0), _player);

            command.Go();

            Assert.AreEqual(_player, _engineer);
        }

        public override void ConsultWith(IEngineer engineer)
        {
            _engineer = engineer;
        }
    }
}
