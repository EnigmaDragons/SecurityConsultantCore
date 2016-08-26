using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTestExtensions;
using SecurityConsultantCore.Domain.Basic;
using SecurityConsultantCore.MapGeneration;

namespace SecurityConsultantCore.Test.MapGeneration
{
    [TestClass]
    public class ObjectInstructionTests
    {
        [TestMethod]
        public void ObjectInstruction_InvalidStringInput_ThrowsArgumentException()
        {
            ExceptionAssert.Throws<ArgumentException>(() => ObjectInstruction.FromString(""));
            ExceptionAssert.Throws<ArgumentException>(() => ObjectInstruction.FromString("(1,1)"));
        }

        [TestMethod]
        public void ObjectInstruction_NoInstructions_ReturnEmptyList()
        {
            Assert.AreEqual(0, ObjectInstruction.FromString("Obj:").Count);
        }

        [TestMethod]
        public void ObjectInstruction_SingleInstruction_IsCorrect()
        {
            var instructions = ObjectInstruction.FromString("Floor: (1,1,U)");

            Assert.AreEqual(1, instructions.Count);
            Assert.AreEqual(Create("Floor", "1,1,U"), instructions[0]);
        }

        [TestMethod]
        public void ObjectInstruction_SingleInstructionTrailingSemicolom_IsCorrect()
        {
            var instructions = ObjectInstruction.FromString("Floor: (1,1,U);");

            Assert.AreEqual(1, instructions.Count);
            Assert.AreEqual(Create("Floor", "1,1,U"), instructions[0]);
        }

        [TestMethod]
        public void ObjectInstruction_MultipleInstructions_IsCorrect()
        {
            var instructions = ObjectInstruction.FromString("Painting1: (4,1,R); (6,1,R)");

            Assert.AreEqual(2, instructions.Count);
            Assert.AreEqual(Create("Painting1", "4,1,R"), instructions[0]);
            Assert.AreEqual(Create("Painting1", "6,1,R"), instructions[1]);
        }

        [TestMethod]
        public void ObjectInstruction_RangeInstruction_IsCorrect()
        {
            var instructions = ObjectInstruction.FromString("Counter: (1,1)-(1,2)");

            Assert.AreEqual(2, instructions.Count);
            Assert.AreEqual(Create("Counter", "1,1"), instructions[0]);
            Assert.AreEqual(Create("Counter", "1,2"), instructions[1]);
        }

        [TestMethod]
        public void ObjectInstruction_MultipleRangeInstruction_IsCorrect()
        {
            var instructions = ObjectInstruction.FromString("Counter: (1,1)-(1,2); (1,5)-(1,6)");

            Assert.AreEqual(4, instructions.Count);
            Assert.AreEqual(Create("Counter", "1,1"), instructions[0]);
            Assert.AreEqual(Create("Counter", "1,2"), instructions[1]);
            Assert.AreEqual(Create("Counter", "1,5"), instructions[2]);
            Assert.AreEqual(Create("Counter", "1,6"), instructions[3]);
        }

        [TestMethod]
        public void ObjectInstruction_DefaultOrientation_AppliesToAllObjects()
        {
            var instructions = ObjectInstruction.FromString("Painting*:(L): (1,1); (1,2)");

            Assert.AreEqual(2, instructions.Count);
            Assert.AreEqual(Create("Painting*", "1,1,L"), instructions[0]);
            Assert.AreEqual(Create("Painting*", "1,2,L"), instructions[1]);
        }

        [TestMethod]
        public void ObjectInstruction_DefaultOrientationWithOverridingObj_ObjOrienationOverridden()
        {
            var instructions = ObjectInstruction.FromString("Painting*:(L): (1,1); (1,2,R)");

            Assert.AreEqual(2, instructions.Count);
            Assert.AreEqual(Create("Painting*", "1,1,L"), instructions[0]);
            Assert.AreEqual(Create("Painting*", "1,2,R"), instructions[1]);
        }

        private ObjectInstruction Create(string objName, string xyo)
        {
            return new ObjectInstruction(objName, XYOrientation.FromString(xyo));
        }
    }
}
