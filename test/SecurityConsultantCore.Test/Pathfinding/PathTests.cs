using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurityConsultantCore.Domain.Basic;
using SecurityConsultantCore.Pathfinding;
using System.Linq;

namespace SecurityConsultantCore.Test.Pathfinding
{
    [TestClass]
    public class PathTests
    {
        [TestMethod]
        public void Path_ValidNodes_IsCorrect()
        {
            var path = new Path(new XYZ(0, 0, 0), new XYZ(0, 1, 0), new XYZ(1, 1, 3));

            Assert.AreEqual(new XYZ(0, 0, 0), path.Origin);
            Assert.AreEqual(new XYZ(1, 1, 3), path.Destination);
            Assert.AreEqual(3, path.Count());
        }
    }
}
