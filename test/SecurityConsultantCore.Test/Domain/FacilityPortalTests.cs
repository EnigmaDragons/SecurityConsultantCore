using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurityConsultantCore.Domain;
using SecurityConsultantCore.Domain.Basic;

namespace SecurityConsultantCore.Test.Domain
{
    [TestClass]
    public class FacilityPortalTests
    {
        [TestMethod]
        public void FacilityPortal_DefaultPortal_IsNonExternalPortalWithNoMapConnections()
        {
            var portal = new FacilityPortal();

            Assert.IsFalse(portal.IsEdgePortal);
            Assert.AreEqual(SpecialLocation.OffOfMap, portal.Endpoint1);
            Assert.AreEqual(SpecialLocation.OffOfMap, portal.Endpoint2);
        }

        [TestMethod]
        public void FacilityPortal_ExternalPortals_AreExternal()
        {
            Assert.IsTrue(new FacilityPortal { Endpoint1 = SpecialLocation.OffOfMap, Endpoint2 = new XYZ(1,1,1)}.IsEdgePortal);
            Assert.IsTrue(new FacilityPortal { Endpoint1 = new XYZ(1, 1, 1), Endpoint2 = SpecialLocation.OffOfMap }.IsEdgePortal);
        }

        [TestMethod]
        public void FacilityPortal_InternalPortal_IsExternalIsFalse()
        {
            var portal = new FacilityPortal { Endpoint1 = new XYZ(1, 1, 1), Endpoint2 = new XYZ(2, 2, 2) };

            Assert.IsFalse(portal.IsEdgePortal);
        }

        [ExpectedException(typeof(InvalidOperationException))]
        [TestMethod]
        public void FacilityPortal_GetDestinationWithNonEndpoint_ThrowsInvalidOperationException()
        {
            var portal = new FacilityPortal { Endpoint1 = new XYZ(1, 1, 1), Endpoint2 = new XYZ(2, 2, 2) };

            portal.GetDestination(new XYZ(1, 2, 3));
        }

        [TestMethod]
        public void FacilityPortal_GetDestination_BothDirectionDestinationsCorrect()
        {
            var portal = new FacilityPortal { Endpoint1 = new XYZ(1, 1, 1), Endpoint2 = new XYZ(2, 2, 2) };

            Assert.AreEqual(portal.Endpoint1, portal.GetDestination(portal.Endpoint2));
            Assert.AreEqual(portal.Endpoint2, portal.GetDestination(portal.Endpoint1));
        }

        [TestMethod]
        public void FacilityPortal_FromFacilityObject_IsCorrect()
        {
            var obj = new FacilityObject {Type = "Stairs", Orientation = Orientation.Left};
            var portal = FacilityPortal.FromObject(obj);

            Assert.AreEqual(obj, (FacilityObject)portal);
        }
    }
}
