using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurityConsultantCore.Domain.Basic;

namespace SecurityConsultantCore.Test.Domain.Basic
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class XYTests
    {
        [TestMethod]
        public void XY_MatchingValues_AreEqual()
        {
            var point1 = new XY(1, 5);
            var point2 = new XY(1, 5);

            Assert.AreEqual(point1, point2);
            Assert.AreEqual(point1.GetHashCode(), point2.GetHashCode());
        }

        [TestMethod]
        public void XY_DifferentValues_AreNotEqual()
        {
            var point1 = new XY(1, 5);
            var point2 = new XY(2, 2);

            Assert.AreNotEqual(point1, point2);
            Assert.AreNotEqual(point1.GetHashCode(), point2.GetHashCode());
        }

        [TestMethod]
        public void XY_Offsets_Correct()
        {
            AssertOffsetEquals(new XY(1, 1), new XY(1, 1), new XY(0, 0));
            AssertOffsetEquals(new XY(0, 0), new XY(2, 3), new XY(2, 3));
            AssertOffsetEquals(new XY(5, 5), new XY(5, 1), new XY(0, -4));
        }

        [TestMethod]
        public void XY_Plus_Correct()
        {
            Assert.AreEqual(new XY(4, 6), new XY(1, 2).Plus(new XY(3, 4)));
            Assert.AreEqual(new XY(10, 16), new XY(2, 7).Plus(new XY(8, 9)));
        }

        [TestMethod]
        public void XY_Thru_Correct()
        {
            var point1 = new XY(1, 3);
            var point2 = new XY(2, 2);

            var range = point1.Thru(point2);

            Assert.AreEqual(4, range.Count);
            Assert.IsTrue(range.Contains(new XY(1, 2)));
            Assert.IsTrue(range.Contains(new XY(1, 3)));
            Assert.IsTrue(range.Contains(new XY(2, 2)));
            Assert.IsTrue(range.Contains(new XY(2, 3)));
        }

        [TestMethod]
        public void XY_IsAdjacentTo_Correct()
        {
            Assert.IsTrue(new XY(0, 0).IsAdjacentTo(new XY(0, 1)));
            Assert.IsTrue(new XY(0, 0).IsAdjacentTo(new XY(1, 0)));
            Assert.IsFalse(new XY(0, 0).IsAdjacentTo(new XY(95, 13)));
            Assert.IsFalse(new XY(0, 0).IsAdjacentTo(new XY(1, 1)));
            Assert.IsFalse(new XY(0, 0).IsAdjacentTo(new XY(1, 20)));
            Assert.IsFalse(new XY(0, 0).IsAdjacentTo(new XY(20, 1)));
            Assert.IsTrue(new XY(-20, -5).IsAdjacentTo(new XY(-21, -5)));
            Assert.IsTrue(new XY(-20, -5).IsAdjacentTo(new XY(-19, -5)));
        }

        private void AssertOffsetEquals(XY src, XY dest, XY expectedOffset)
        {
            Assert.AreEqual(expectedOffset, src.GetOffset(dest));
        }
    }
}
