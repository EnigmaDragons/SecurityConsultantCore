﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        [TestMethod]
        public void EraseCommand_EraseSecurityObjectAtLocation()
        {
            var map = new FacilityMap(new InMemoryWorld());
            map.Add(new FacilityLayer(1, 1));
            map[0, 0, 0].Put(new FakeSecurityObject { ObjectLayer = ObjectLayer.LowerObject });
            var command = new EraseCommand(map, new XYZ(0, 0, 0));

            command.Go();

            Assert.AreEqual(map[0, 0, 0].LowerObject.Type, "None");
        }
    }
}