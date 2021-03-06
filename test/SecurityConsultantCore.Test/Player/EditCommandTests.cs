﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurityConsultantCore.Domain;
using SecurityConsultantCore.Domain.Basic;
using SecurityConsultantCore.Player;
using SecurityConsultantCore.Test.Engine;
using SecurityConsultantCore.Test._TestDoubles;

namespace SecurityConsultantCore.Test.Player
{
    [TestClass]
    public class EditCommandTests : SecurityObjectBase
    {
        private FacilityMap _map;
        private FacilityLayer _layer;

        private IEngineer _engineer;
        private readonly FakeEngineer _player = new FakeEngineer();
        
        [TestInitialize]
        public void Init()
        {
            ObjectLayer = ObjectLayer.GroundPlaceable;
            _map = new FacilityMap(new InMemoryWorld());
            _layer = new FacilityLayer(2, 2);
            _map.Add(_layer);
        }

        [TestMethod]
        public void EditCommand_EditEmptySpace_NoObjectConsultsWithPlayer()
        {
            var command = new EditCommand(_map, new XYZ(1, 1, 0), _player);

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

        [TestMethod]
        public void EditCommand_EditOutsideOfMap_NoExceptionsThrown()
        {
            var command = new EditCommand(_map, SpecialLocation.OffOfMap, _player);

            command.Go();
        }

        public override void ConsultWith(IEngineer engineer)
        {
            _engineer = engineer;
        }
    }
}
