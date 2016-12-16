using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurityConsultantCore.Spatial;

namespace SecurityConsultantCore.Test.Spatial
{
    [TestClass]
    public class VolumeTests
    {
        [TestMethod]
        public void Volume_Created_ResultsCorrect()
        {
            var volume = new Volume(new bool[2, 2] { { true, false }, { false, true } });

            Assert.IsTrue(volume[0, 0]);
            Assert.IsFalse(volume[1, 0]);
            Assert.IsFalse(volume[0, 1]);
            Assert.IsTrue(volume[1, 1]);
        }

        [TestMethod]
        public void Volume_Equals_ResultsCorrect()
        {
            var volume1 = new Volume(new bool[2, 2] { { true, false }, { false, true } });
            var volume2 = new Volume(new bool[2, 2] { { true, false }, { false, true } });

            Assert.AreEqual(volume1, volume2);
        }
    }
}
