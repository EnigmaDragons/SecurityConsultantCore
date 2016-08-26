using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurityConsultantCore.Domain.Basic;

namespace SecurityConsultantCore.Test.Domain.Basic
{
    [TestClass]
    public class XYRangeTests
    {
        [TestMethod]
        public void XYRange_SingleSquare_TotalAreaEqualsOne()
        {
            var range = new XYRange(new XY(0, 0), new XY(0, 0));

            Assert.AreEqual(1, range.Count());
        }
    }
}
