using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTestExtensions;
using SecurityConsultantCore.Domain.Basic;
using SecurityConsultantCore.OOMath;

namespace SecurityConsultantCore.Test.Domain.Basic
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class XYOrientationTests
    {
        [TestMethod]
        public void XYOrientation_InvalidStringInputs_ThrowsArgumentException()
        {
            ExceptionAssert.Throws(() => XYOrientation.FromString(""));
            ExceptionAssert.Throws(() => XYOrientation.FromString("12"));
        }

        [TestMethod]
        public void XYOrientation_ValidXYStringInput_IsCorrect()
        { 
            var xyo = XYOrientation.FromString("(1,2)");

            Assert.AreEqual((Number)1, xyo.X);
            Assert.AreEqual((Number)2, xyo.Y);
            Assert.AreEqual(Orientation.None, xyo.Orientation);
        }

        [TestMethod]
        public void XYOrientation_ValidXYOStringInput_IsCorrect()
        {
            var xyo = XYOrientation.FromString("(3,4,D)");
            
            Assert.AreEqual((Number)3, xyo.X);
            Assert.AreEqual((Number)4, xyo.Y);
            Assert.AreEqual(Orientation.Down, xyo.Orientation);
        }

        [TestMethod]
        public void XYOrientation_SameValues_AreEqual()
        {
            Assert.AreEqual(new XYOrientation(1, 2), new XYOrientation(1, 2));
        }
    }
}
