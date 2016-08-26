using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurityConsultantCore.Domain.Basic;

namespace SecurityConsultantCore.Test.Domain.Basic
{
    [TestClass]
    public class XYZTests
    {
        [TestMethod]
        public void XYZ_MatchingValues_AreEqual()
        {
            var point1 = new XYZ(1, 2, 3);
            var point2 = new XYZ(1, 2, 3);

            Assert.AreEqual(point1, point2);
            Assert.AreEqual(point1.GetHashCode(), point2.GetHashCode());
        }

        [TestMethod]
        public void XYZ_DifferentValues_AreNotEqual()
        {
            var point1 = new XYZ(1, 2, 3);
            var point2 = new XYZ(4, 5, 6);

            Assert.AreNotEqual(point1, point2);
            Assert.AreNotEqual(point1.GetHashCode(), point2.GetHashCode());
        }
    }
}
