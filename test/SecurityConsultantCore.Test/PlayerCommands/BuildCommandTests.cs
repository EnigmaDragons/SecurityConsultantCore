using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurityConsultantCore.Domain;
using SecurityConsultantCore.Domain.Basic;
using SecurityConsultantCore.PlayerCommands;
using SecurityConsultantCore.Test.EngineMocks;

namespace SecurityConsultantCore.Test.PlayerCommands
{
    [TestClass]
    public class BuildCommandTests
    {
        [TestMethod]
        public void BuildCommand_Build_ObjectPlaced()
        {
            var map = new FacilityMap(new InMemoryWorld());
            map.Add(new FacilityLayer(3, 3));
            var securityObj = new SecurityObject { ObjectLayer = ObjectLayer.LowerObject };
            var command = new BuildCommand(map, securityObj, new XYZ(0, 0, 0));

            command.Go();

            Assert.AreEqual(map[0, 0, 0].LowerObject, securityObj);
        }
    }
}
