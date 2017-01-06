using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurityConsultantCore.Domain.Basic;
using SecurityConsultantCore.Spatial;
using System.Collections.Generic;
using System.Linq;

namespace SecurityConsultantCore.Test.Spatial
{
    [TestClass]
    public class VolumeTests
    {
        [TestMethod]
        public void Volume_GetOccupiedSpaces_SpacesCorrect()
        {
            var volume = new Volume(new List<XY> { new XY(0, 0), new XY(1, 1) });

            var spaces = volume.GetOccupiedSpaces(new XYZOrientation(0, 0, 0, Orientation.Default)).ToList();

            CollectionAssert.AreEquivalent(new List<XYZ> { new XYZ(0, 0, 0), new XYZ(1, 1, 0) }, spaces);
        }

        [TestMethod]
        public void Volume_GetRotatedOccupiedSpaces_SpacesCorrect()
        {
            var volume = new Volume(new List<XY> { new XY(0, 0), new XY(1, 1) });

            var spaces = volume.GetOccupiedSpaces(new XYZOrientation(0, 0, 0, Orientation.Ninety)).ToList();

            var expected = new List<XYZ> { new XYZ(0, 0, 0), new XYZ(-1, 1, 0) };
            Assert.IsTrue(expected.TrueForAll(x => spaces.Contains(x)));
        }
    }
}
