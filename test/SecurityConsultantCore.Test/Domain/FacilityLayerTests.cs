using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTestExtensions;
using SecurityConsultantCore.Domain;
using SecurityConsultantCore.Domain.Basic;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace SecurityConsultantCore.Test.Domain
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class FacilityLayerTests
    {
        private FacilityLayer _layer;
        private ValuableFacilityObject _sampleLowerValuable;
        private ValuableFacilityObject _sampleLowerValuable2;
        private ValuableFacilityObject _sampleLowerValuable3;
        private ValuableFacilityObject _sampleLowerValuable4;
        private ValuableFacilityObject _sampleUpperValuable;
        private FacilityPortal _sampleGroundPortal;
        private FacilityPortal _sampleUpperPortal;

        [TestInitialize]
        public void Init()
        {
            _layer = new FacilityLayer();
            _sampleLowerValuable = new ValuableFacilityObject { Type = "CouchLeft" };
            _sampleLowerValuable2 = new ValuableFacilityObject { Type = "CouchRight" };
            _sampleLowerValuable3 = new ValuableFacilityObject { Type = "Cash" };
            _sampleLowerValuable4 = new ValuableFacilityObject { Type = "Sapphire" };
            _sampleUpperValuable = new ValuableFacilityObject { Type = "Wallet" };
            _sampleGroundPortal = new FacilityPortal { Type = "Manhole" };
            _sampleUpperPortal = new FacilityPortal { Type = "Door" };
        }

        [TestMethod]
        public void FacilityLayer_PutObjectInLayer_ObjectIsInLayer()
        {
            _layer.Put(new XYOrientation(1, 2), _sampleLowerValuable);

            Assert.IsTrue(_layer.Valuables.Any(x => x.Obj.Equals(_sampleLowerValuable)));
        }

        [TestMethod]
        public void FacilityLayer_CreateCustomSizeLayer_SizeIsCorrect()
        {
            var layer = new FacilityLayer(50, 50);

            Assert.AreEqual(new XY(50, 50), layer.Size);
        }

        [TestMethod]
        public void FacilityLayer_TwoValuables_ValuablesCorrect()
        {
            var layer = new FacilityLayer(3, 3);
            layer.Put(new XYOrientation(1, 1), _sampleLowerValuable);
            layer.Put(new XYOrientation(2, 2), _sampleUpperValuable);

            var valuables = layer.Valuables.ToList();

            Assert.AreEqual(2, valuables.Count());
            Assert.IsTrue(valuables.Any(x => x.Obj.Equals(_sampleLowerValuable)));
            Assert.IsTrue(valuables.Any(x => x.Obj.Equals(_sampleUpperValuable)));
        }

        [TestMethod]
        public void FacilityLayer_ValuableOutOfBOunds_ThrowException()
        {
            var layer = new FacilityLayer(1, 1);

            ExceptionAssert.Throws<InvalidOperationException>(() => layer.Put(new XYOrientation(1.5, 1.5), _sampleUpperValuable));
        }

        [TestMethod]
        public void FacilityLayer_MultipleValuables_ValuablesCorrect()
        {
            var layer = new FacilityLayer(3, 3);
            layer.Put(new XYOrientation(0, 0), _sampleLowerValuable);
            layer.Put(new XYOrientation(0, 1), _sampleLowerValuable2);
            layer.Put(new XYOrientation(1, 1), _sampleLowerValuable3);
            layer.Put(new XYOrientation(1, 2), _sampleLowerValuable4);

            var valuables = layer.Valuables;

            Assert.AreEqual(4, valuables.Count());
        }

        [TestMethod]
        public void FacilityLayer_TwoPortals_PortalsCorrect()
        {
            var layer = new FacilityLayer(2, 1);
            layer.Put(new XYOrientation(0, 0), _sampleUpperPortal);
            layer.Put(new XYOrientation(0, 0), _sampleGroundPortal);

            var portals = layer.Portals.ToList();

            Assert.AreEqual(2, portals.Count());
            Assert.IsTrue(portals.Any(x => x.Obj.Equals(_sampleUpperPortal)));
            Assert.IsTrue(portals.Any(x => x.Obj.Equals(_sampleGroundPortal)));
        }


        [TestMethod]
        public void FacilityLayer_RemoveValuable_ValuableRemoved()
        {
            var layer = new FacilityLayer(3, 3);
            layer.Put(new XYOrientation(0, 0), _sampleLowerValuable);

            layer.Remove(_sampleLowerValuable);

            Assert.AreEqual(0, layer.Valuables.Count());
        }
    }
}
