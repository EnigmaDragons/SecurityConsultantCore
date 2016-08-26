using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTestExtensions;
using SecurityConsultantCore.Domain;
using SecurityConsultantCore.Domain.Basic;
using SecurityConsultantCore.MapGeneration;

namespace SecurityConsultantCore.Test.MapGeneration
{
    [TestClass]
    public class PortalInstructionTests
    {
        [TestMethod]
        public void PortalInstruction_IncompleteInsructionFromString_ThrowsArgumentException()
        {
            ExceptionAssert.Throws<ArgumentException>(() => PortalInstruction.FromString(""));
            ExceptionAssert.Throws<ArgumentException>(() => PortalInstruction.FromString("Portal-Door:"));
            ExceptionAssert.Throws<ArgumentException>(() => PortalInstruction.FromString("Portal-Door: (1,1,D)"));
            ExceptionAssert.Throws<ArgumentException>(() => PortalInstruction.FromString("Portal- End1"));
            ExceptionAssert.Throws<ArgumentException>(() => PortalInstruction.FromString("Portal- End2"));
            ExceptionAssert.Throws<ArgumentException>(() => PortalInstruction.FromString("End1 End2"));
        }

        [TestMethod]
        public void PortalInstruction_FromString_IsCorrect()
        {
            var portal = PortalInstruction.FromString("Portal-Door: (1,1,1); End1=(1,2,0); End2=(0,1,0)");

            Assert.AreEqual("Door", portal.Type);
            Assert.AreEqual(XYZOrientation.FromString("1,1,1,U"), portal.Location);
            Assert.AreEqual(new XYZ(1, 2, 0), portal.Endpoint1);
            Assert.AreEqual(new XYZ(0, 1, 0), portal.Endpoint2);
        }

        [TestMethod]
        public void PortalInstruction_OffMapEndpointFromString_IsCorrect()
        {
            var portal = PortalInstruction.FromString("Portal-Door: (1,1,1); End1=(1,2,0); End2=(OffMap)");

            Assert.AreEqual("Door", portal.Type);
            Assert.AreEqual(XYZOrientation.FromString("1,1,1,U"), portal.Location);
            Assert.AreEqual(new XYZ(1, 2, 0), portal.Endpoint1);
            Assert.AreEqual(SpecialLocation.OffOfMap, portal.Endpoint2);
        }
    }
}
