using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurityConsultantCore.Domain.Basic;

namespace SecurityConsultantCore.Test.Domain.Basic
{
    [TestClass]
    public class XYZWTests
    {
        [TestMethod]
        public void XYZW_EnumSameValues_AreEqual()
        {
            var loc1 = new XYZW<ObjectLayer>(1, 2, 3, ObjectLayer.Ground);
            var loc2 = new XYZW<ObjectLayer>(1, 2, 3, ObjectLayer.Ground);

            Assert.AreEqual(loc1, loc2);
            Assert.AreEqual(loc1.GetHashCode(), loc2.GetHashCode());
        }

        [TestMethod]
        public void XYZW_EnumDifferentValues_AreNotEqual()
        {
            var loc1 = new XYZW<ObjectLayer>(1, 2, 3, ObjectLayer.Ground);
            var loc2 = new XYZW<ObjectLayer>(1, 2, 3, ObjectLayer.LowerObject);

            Assert.AreNotEqual(loc1, loc2);
            Assert.AreNotEqual(loc1.GetHashCode(), loc2.GetHashCode());
        }
    }
}
