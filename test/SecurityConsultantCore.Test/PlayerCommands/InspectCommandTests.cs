using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurityConsultantCore.Domain;
using SecurityConsultantCore.Domain.Basic;
using SecurityConsultantCore.PlayerCommands;
using System.Linq;
using System;
using System.Collections.Generic;
using SecurityConsultantCore.Test.EngineMocks;

namespace SecurityConsultantCore.Test.PlayerCommands
{
    [TestClass]
    public class InspectCommandTests : IInspector
    {
        private readonly List<ValuableFacilityObject> valuables = new List<ValuableFacilityObject>();
        private readonly List<ValuablesContainer> containers = new List<ValuablesContainer>();

        private FacilityMap map;
        private FacilityLayer layer;

        [TestInitialize]
        public void Init()
        {
            map = new FacilityMap(new InMemoryWorld());
            layer = new FacilityLayer(2, 2);
            map.Add(layer);
        }

        [TestMethod]
        public void InspectCommand_InspectOffMap_NoExceptionsThrown()
        {
            new InspectCommand(map, SpecialLocation.OffOfMap, this).Go();
        }

        [TestMethod]
        public void InspectCommand_InspectSpaceWithValuable_NotifiesValuable()
        {
            var valuable = new ValuableFacilityObject { ObjectLayer = ObjectLayer.LowerObject };
            layer[0, 0].Put(valuable);
            var command = new InspectCommand(map, new XYZ(0, 0, 0), this);

            command.Go();

            Assert.AreEqual(valuable, valuables.First());
        }

        [TestMethod]
        public void InspectCommand_InspectSpaceWithContainer_NotifiesContainer()
        {
            var container = new ValuablesContainer { ObjectLayer = ObjectLayer.LowerObject };
            layer[0, 0].Put(container);
            var command = new InspectCommand(map, new XYZ(0, 0, 0), this);

            command.Go();

            Assert.AreEqual(container, containers.First());
        }

        public void Notify(ValuableFacilityObject valuable)
        {
            valuables.Add(valuable);
        }

        public void Notify(ValuablesContainer container)
        {
            containers.Add(container);
        }
    }
}
