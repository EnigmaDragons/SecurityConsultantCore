using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTestExtensions;
using SecurityConsultantCore.Domain.Basic;
using SecurityConsultantCore.MapGeneration;

namespace SecurityConsultantCore.Test.MapGeneration
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class LinkInstructionTests
    {
        [TestMethod]
        public void LinkInstruction_IncompleteInstructionFromString_ThrowsArgumentException()
        {
            ExceptionAssert.Throws<ArgumentException>(() => LinkInstruction.FromString(""));
            ExceptionAssert.Throws<ArgumentException>(() => LinkInstruction.FromString("Link:"));
            ExceptionAssert.Throws<ArgumentException>(() => LinkInstruction.FromString("(Obj1,1,1)"));
            ExceptionAssert.Throws<ArgumentException>(() => LinkInstruction.FromString("Link:(Obj1,1,1)"));
        }

        [TestMethod]
        public void LinkInstruction_InstructionFromString_IsCorrect()
        {
            var inst = LinkInstruction.FromString("Link:(Obj1,1,1)-(Obj1,1,2)");

            Assert.AreEqual("Obj1", inst.Obj1.Obj);
            Assert.AreEqual(new XY(1, 1), inst.Obj1.Location);
            Assert.AreEqual("Obj1", inst.Obj2.Obj);
            Assert.AreEqual(new XY(1, 2), inst.Obj2.Location);
        }

        [TestMethod]
        public void LinkInstruction_CondensedInstructionFromString_IsCorrect()
        {
            var inst = LinkInstruction.FromString("Obj1,1,1-Obj1,1,2");

            Assert.AreEqual("Obj1", inst.Obj1.Obj);
            Assert.AreEqual(new XY(1, 1), inst.Obj1.Location);
            Assert.AreEqual("Obj1", inst.Obj2.Obj);
            Assert.AreEqual(new XY(1, 2), inst.Obj2.Location);
        }
    }
}
