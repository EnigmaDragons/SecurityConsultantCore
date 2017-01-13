using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurityConsultantCore.Domain;
using SecurityConsultantCore.Domain.Basic;
using SecurityConsultantCore.Player;
using SecurityConsultantCore.Test.Engine;

namespace SecurityConsultantCore.Test.Player
{
    [TestClass]
    public class InspectCommandTests : IInspector
    {
        private readonly List<ValuableFacilityObject> _valuables = new List<ValuableFacilityObject>();
        private readonly List<ValuablesContainer> _containers = new List<ValuablesContainer>();

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
        public void InspectCommand_InspectOffMap_NoExceptionsThrown()
        {
            new InspectCommand(_map, SpecialLocation.OffOfMap, this).Go();
        }

        [TestMethod]
        public void InspectCommand_InspectSpaceWithValuable_NotifiesValuable()
        {
            var valuable = new ValuableFacilityObject { ObjectLayer = ObjectLayer.LowerObject };
            _layer[0, 0].Put(valuable);
            var command = new InspectCommand(_map, new XYZ(0, 0, 0), this);

            command.Go();

            Assert.AreEqual(valuable, _valuables.First());
        }

        [TestMethod]
        public void InspectCommand_InspectSpaceWithContainer_NotifiesContainer()
        {
            var container = new ValuablesContainer { ObjectLayer = ObjectLayer.LowerObject };
            _layer[0, 0].Put(container);
            var command = new InspectCommand(_map, new XYZ(0, 0, 0), this);

            command.Go();

            Assert.AreEqual(container, _containers.First());
        }

        public void Notify(ValuableFacilityObject valuable)
        {
            _valuables.Add(valuable);
        }

        public void Notify(ValuablesContainer container)
        {
            _containers.Add(container);
        }
    }
}
