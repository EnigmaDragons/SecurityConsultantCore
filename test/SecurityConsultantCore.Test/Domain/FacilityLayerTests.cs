using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurityConsultantCore.Domain;
using SecurityConsultantCore.Domain.Basic;

namespace SecurityConsultantCore.Test.Domain
{
    [TestClass]
    public class FacilityLayerTests
    {
        private FacilityLayer _layer;
        private FacilitySpace _sampleSpace;
        private ValuableFacilityObject _sampleLowerValuable;
        private ValuableFacilityObject _sampleLowerValuable2;
        private ValuableFacilityObject _sampleUpperValuable;
        private FacilityPortal _sampleGroundPortal;
        private FacilityPortal _sampleUpperPortal;

        [TestInitialize]
        public void Init()
        {
            _layer = new FacilityLayer();
            _sampleSpace = new FacilitySpace();
            _sampleLowerValuable = new ValuableFacilityObject { Type = "CouchLeft", ObjectLayer = ObjectLayer.LowerObject };
            _sampleLowerValuable2 = new ValuableFacilityObject { Type = "CouchRight", ObjectLayer = ObjectLayer.LowerObject };
            _sampleUpperValuable = new ValuableFacilityObject { Type = "Wallet", ObjectLayer = ObjectLayer.UpperObject };
            _sampleGroundPortal = new FacilityPortal {Type = "Manhole", ObjectLayer = ObjectLayer.Ground};
            _sampleUpperPortal = new FacilityPortal { Type = "Door", ObjectLayer = ObjectLayer.UpperObject };
        }

        [TestMethod]
        public void FacilityLayer_CreateCustomSizeLayer_SizeIsCorrect()
        {
            var layer = new FacilityLayer(50, 50);

            Assert.AreEqual(new XY(50, 50), layer.Size);
        }

        [TestMethod]
        public void FacilityLayer_PutFacilitySpaceWithXY_CanGetSpace()
        {
            _layer.Put(new XY(5, 6), _sampleSpace);

            Assert.AreEqual(_sampleSpace, _layer[5, 6]);
        }

        [TestMethod]
        public void FacilityLayer_PutFacilitySpace_CanGetSpace()
        {
            _layer.Put(10, 8, _sampleSpace);

            Assert.AreEqual(_sampleSpace, _layer[10, 8]);
        }

        [TestMethod]
        public void FacilityLayer_GetNonExistentSpace_ReturnsEmptySpace()
        {
            var layer = new FacilityLayer();

            Assert.IsTrue(layer[1, 1].IsEmpty);
        }

        [TestMethod]
        public void FacilityLayer_IterateEmptyLayer_NoSpaces()
        {
            var iterator = new FacilityLayer(0, 0);

            Assert.AreEqual(0, iterator.Count());
        }

        [TestMethod]
        public void FacilityLayer_IterateSmallLayer_IteratesCorrectly()
        {
            var iterator = new FacilityLayer(2, 2);

            var spaces = iterator.ToList();

            Assert.AreEqual(4, spaces.Count);
            Assert.AreEqual(new XY(0, 0), spaces[0].Location);
            Assert.AreEqual(new XY(1, 0), spaces[1].Location);
            Assert.AreEqual(new XY(0, 1), spaces[2].Location);
            Assert.AreEqual(new XY(1, 1), spaces[3].Location);
        }

        [TestMethod]
        public void FacilityLayer_TwoValuables_ValuablesCorrect()
        {
            var layer = new FacilityLayer(1, 1);
            layer[0, 0].LowerObject = _sampleLowerValuable;
            layer[0, 0].UpperObject = _sampleUpperValuable;

            var valuables = layer.LocatedValuables.ToList();

            Assert.AreEqual(2, valuables.Count());
            Assert.IsTrue(valuables.Any(x => x.Obj.Equals(_sampleLowerValuable)));
            Assert.IsTrue(valuables.Any(x => x.Obj.Equals(_sampleUpperValuable)));
        }

        [TestMethod]
        public void FacilityLayer_MultipleValuables_ValuablesCorrect()
        {
            var layer = new FacilityLayer(3, 3);
            layer[0, 0].Put(_sampleLowerValuable);
            layer[1, 0].Put(_sampleLowerValuable);
            layer[1, 2].Put(_sampleLowerValuable);
            layer[2, 2].Put(_sampleLowerValuable);

            var valuables = layer.LocatedValuables;

            Assert.AreEqual(4, valuables.Count());
        }

        [TestMethod]
        public void FacilityLayer_TwoPortals_PortalsCorrect()
        {
            var layer = new FacilityLayer(2, 1);
            layer[0, 0].UpperObject = _sampleUpperPortal;
            layer[1, 0].UpperObject = _sampleGroundPortal;

            var portals = layer.Portals.ToList();

            Assert.AreEqual(2, portals.Count());
            Assert.IsTrue(portals.Any(x => x.Obj.Equals(_sampleUpperPortal)));
            Assert.IsTrue(portals.Any(x => x.Obj.Equals(_sampleGroundPortal)));
        }

        [TestMethod]
        public void FacilityLayer_MultiplePortals_PortalsCorrect()
        {
            var layer = new FacilityLayer(3, 3);
            layer[0, 0].Put(_sampleUpperPortal);
            layer[1, 0].Put(_sampleUpperPortal);
            layer[1, 2].Put(_sampleUpperPortal);
            layer[2, 2].Put(_sampleUpperPortal);

            var portals = layer.Portals;

            Assert.AreEqual(4, portals.Count());
        }

        [TestMethod]
        public void FacilityLayer_SpaceDoesNotExist_ReturnsFalse()
        {
            var layer = new FacilityLayer(3, 3);

            Assert.IsFalse(layer.Exists(new XY(4, 4)));
        }

        [TestMethod]
        public void FacilityLayer_SpaceExists_ReturnsTrue()
        {
            var layer = new FacilityLayer(3, 3);

            Assert.IsTrue(layer.Exists(new XY(2, 2)));
        }

        [TestMethod]
        public void FacilityLayer_RemoveValuable_ValuableRemoved()
        {
            var layer = new FacilityLayer(3, 3);
            layer[0, 0].Put(_sampleLowerValuable);

            layer.Remove(_sampleLowerValuable);

            Assert.AreEqual(new FacilityObject(), layer[0, 0].LowerObject);
        }

        [TestMethod]
        public void FacilityLayer_RemoveLinkedObject_ObjectRemovedFromBothSpots()
        {
            var layer = new FacilityLayer(3, 3);
            _sampleLowerValuable.LinkTo(_sampleLowerValuable2);
            layer[0, 0].Put(_sampleLowerValuable);
            layer[0, 1].Put(_sampleLowerValuable2);

            layer.Remove(_sampleLowerValuable);

            Assert.AreEqual(new FacilityObject(), layer[0, 0].LowerObject);
            Assert.AreEqual(new FacilityObject(), layer[0, 1].LowerObject);
        }
    }
}
