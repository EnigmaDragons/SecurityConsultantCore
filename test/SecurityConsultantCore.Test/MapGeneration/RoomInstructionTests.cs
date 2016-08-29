using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTestExtensions;
using SecurityConsultantCore.MapGeneration;

namespace SecurityConsultantCore.Test.MapGeneration
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class RoomInstructionTests
    {
        [TestMethod]
        public void RoomInstructions_InvalidInputStrings_ThrowsArgumentException()
        {
            ExceptionAssert.Throws<ArgumentException>(() => RoomInstruction.FromStrings(CreateLines()));
            ExceptionAssert.Throws<ArgumentException>(() => RoomInstruction.FromStrings(CreateLines("Obj:(1,1)")));
        }

        [TestMethod]
        public void RoomInstruction_OneObjectInstruction_InstructionsCorrect()
        {
            var room = RoomInstruction.FromStrings(CreateLines("Room:(0,0)", "Obj:(1,1)"));

            Assert.AreEqual(1, room.ObjectInstructions.Count);
        }

        [TestMethod]
        public void RoomInstruction_TwoObjectInstructionLines_InstructionsCorrect()
        {
            var room = RoomInstruction.FromStrings(CreateLines("Room:(0,0)", "Obj1:(1,1);(1,2)", "Obj2:(1,3)"));

            Assert.AreEqual(3, room.ObjectInstructions.Count);
        }

        [TestMethod]
        public void RoomInstruction_WithRangeInstruction_InstructionsCorrect()
        {
            var room = RoomInstruction.FromStrings(CreateLines("Room:(0,0)", "Floor:(1,1)-(3,3)"));

            Assert.AreEqual(9, room.ObjectInstructions.Count);
        }

        [TestMethod]
        public void RoomInstruction_WithCommentAndEmptyLine_InstructionsCorrect()
        {
            var room = RoomInstruction.FromStrings(CreateLines("// Bathroom //", "Room:(0,0)", "", "Floor:(1,1)"));

            Assert.AreEqual(1, room.ObjectInstructions.Count);
        }

        private List<string> CreateLines(params string[] args)
        {
            return args.ToList();
        }
    }
}
