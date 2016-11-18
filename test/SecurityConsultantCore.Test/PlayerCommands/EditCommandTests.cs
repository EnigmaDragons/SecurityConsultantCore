using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurityConsultantCore.Domain;
using SecurityConsultantCore.Domain.Basic;
using SecurityConsultantCore.PlayerCommands;
using SecurityConsultantCore.Test.EngineMocks;

namespace SecurityConsultantCore.Test.PlayerCommands
{
    [TestClass]
    public class EditCommandTests : IPlayerEdit
    {
        private FacilityMap _map;
        private FacilityLayer _layer;

        [TestInitialize]
        public void Init()
        {
            _map = new FacilityMap(new InMemoryWorld());
            _layer = new FacilityLayer(2, 2);
            _map.Add(_layer);
        }

        [TestMethod]
        public void EditCommand_GoWithNoObjectOnSpace_NothingHappens()
        {
            var command = new EditCommand(new FacilityMap(new InMemoryWorld()), new XYZ(1, 1, 1), this);

            command.Go();
        }

        [TestMethod]
        public void EditCommand_GoWithObjectOnSpace_SomethingHappens()
        {
//            _layer[1, 1].Put();
//            var command = new EditCommand(_map, );
        }

        public void PresentOption()
        {
            throw new System.NotImplementedException();
        }
    }
}
