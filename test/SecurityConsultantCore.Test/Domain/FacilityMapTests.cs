using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurityConsultantCore.Domain;
using SecurityConsultantCore.Domain.Basic;

namespace SecurityConsultantCore.Test.Domain
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class FacilityMapTests
    {
        private FacilityMap _map;
        private FacilityLayer _sampleLayer;
        private FacilitySpace _sampleSpace;

        [TestInitialize]
        public void Init()
        {
            _map = new FacilityMap();
            _sampleLayer = new FacilityLayer();
            _sampleSpace = new FacilitySpace();
        }

        [TestMethod]
        public void FacilityMap_AddLayer_CanGetLayer()
        {
            _map.Add(_sampleLayer);

            Assert.AreEqual(_sampleLayer, _map[0]);
        }

        [TestMethod]
        public void FacilityMap_GetFirstLayerSpace_CanGetSpace()
        {
            _sampleLayer.Put(1, 2, _sampleSpace);

            _map.Add(_sampleLayer);

            Assert.AreEqual(_sampleSpace, _map[1, 2, 0]);
        }

        [TestMethod]
        public void FacilityMap_GetThirdLayerSpace_CanGetSpace()
        {
            var thirdFloor = new FacilityLayer();
            thirdFloor.Put(13, 19, _sampleSpace);

            _map.Add(_sampleLayer);
            _map.Add(_sampleLayer);
            _map.Add(thirdFloor);

            Assert.AreEqual(_sampleSpace, _map[13, 19, 2]);
        }

        [TestMethod]
        public void FacilityMap_GetSpaceUsingXYZ_CanGetSpace()
        {
            _sampleLayer.Put(9, 3, _sampleSpace);

            _map.Add(_sampleLayer);

            Assert.AreEqual(_sampleSpace, _map[new XYZ(9, 3, 0)]);
        }

        [TestMethod]
        public void FacilityMap_Valuables_AreCorrect()
        {
            _map.Add(_sampleLayer);
            var value1 = new ValuableFacilityObject { Type = "Painting", ObjectLayer = ObjectLayer.UpperObject };
            var container = new ValuablesContainer { Type = "Table", ObjectLayer = ObjectLayer.LowerObject };
            var value2 = new Valuable { Type = "Diamond"};
            container.Put(value2);
            _map[5, 5, 0].Put(value1);
            _map[7, 8, 0].Put(container);

            var valuables = _map.Valuables.ToList();

            Assert.AreEqual(2, valuables.Count);
            Assert.IsTrue(valuables.Any(x => x.Type.Equals("Painting")));
            Assert.IsTrue(valuables.Any(x => x.Type.Equals("Diamond")));
        }

        [TestMethod]
        public void FacilityMap_Portals_AreCorrect()
        {
            _map.Add(_sampleLayer);
            var value1 = new FacilityPortal { Type = "Door", ObjectLayer = ObjectLayer.UpperObject };
            var value2 = new FacilityPortal { Type = "Window", ObjectLayer = ObjectLayer.UpperObject };
            _map[5, 5, 0].Put(value1);
            _map[7, 8, 0].Put(value2);

            var portals = _map.Portals.ToList();

            Assert.AreEqual(2, portals.Count());
            Assert.AreEqual(value1, portals.First(x => x.Location.Equals(new XYZ(5, 5, 0))).Obj);
            Assert.AreEqual(value2, portals.First(x => x.Location.Equals(new XYZ(7, 8, 0))).Obj);
        }

        [TestMethod]
        public void FacilityMap_Exists_ReturnsFalse()
        {
            _sampleLayer.Put(1, 1, _sampleSpace);

            _map.Add(_sampleLayer);

            Assert.IsFalse(_map.Exists(new XYZ(1, 1, 2)));
        }

        [TestMethod]
        public void FacilityMap_Exits_ReturnsTrue()
        {
            _sampleLayer.Put(1, 1, _sampleSpace);

            _map.Add(_sampleLayer);

            Assert.IsFalse(_map.Exists(new XYZ(1, 1, 1)));
        }
    }
}
