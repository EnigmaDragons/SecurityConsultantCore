using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurityConsultantCore.Domain.Basic;

namespace SecurityConsultantCore.Test.Domain.Basic
{
    [TestClass]
    public class XYZAdjacentTests
    {
        [TestMethod]
        public void XYZAdjacent_ToTheRightOf_IsCorrect()
        {
            Assert.IsTrue(new XYZAdjacent(new XYZ(0, 1, 0), Orientation.Right).Equals(new XYZ(1, 1, 0)));
        }

        [TestMethod]
        public void XYZAdjacent_ToTheLeftOf_IsCorrect()
        {
            Assert.IsTrue(new XYZAdjacent(new XYZ(2, 1, 0), Orientation.Left).Equals(new XYZ(1, 1, 0)));
        }

        [TestMethod]
        public void XYZAdjacent_Above_IsCorrect()
        {
            Assert.IsTrue(new XYZAdjacent(new XYZ(1, 2, 0), Orientation.Up).Equals(new XYZ(1, 1, 0)));
        }

        [TestMethod]
        public void XYZAdjacent_Below_IsCorrect()
        {
            Assert.IsTrue(new XYZAdjacent(new XYZ(1, 0, 0), Orientation.Down).Equals(new XYZ(1, 1, 0)));
        }
    }
}
