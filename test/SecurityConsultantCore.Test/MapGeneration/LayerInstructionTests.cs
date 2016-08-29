using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTestExtensions;
using SecurityConsultantCore.Domain.Basic;
using SecurityConsultantCore.MapGeneration;

namespace SecurityConsultantCore.Test.MapGeneration
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class LayerInstructionTests
    {
        [TestMethod]
        public void LayerInstruction_InvalidLines_ThrowsArgumentException()
        {
            ExceptionAssert.Throws<ArgumentException>(() => LayerInstruction.FromStrings(CreateLines()));
            ExceptionAssert.Throws<ArgumentException>(() => LayerInstruction.FromStrings(CreateLines("Room: (0,0)", "Floor: (1,1)")));
        }

        [TestMethod]
        public void LayerInstruction_EmptyLayerWithSize_SizeCorrect()
        {
            var layer = LayerInstruction.FromStrings(CreateLines("Layer: Size=(4,5)"));

            Assert.AreEqual(new XY(4, 5), layer.Size);
        }

        [TestMethod]
        public void LayerInstruction_OneRoomFromStrings_IsCorrect()
        {
            var layer = LayerInstruction.FromStrings(CreateLines(
                "Layer: Size=(3,3)", "Room: (0,0)", "Floor: (1,1)"));

            Assert.AreEqual(1, layer.Rooms.Count);
            Assert.AreEqual(new XY(0, 0), layer.Rooms[0].Location);
            Assert.AreEqual(1, layer.Rooms[0].ObjectInstructions.Count);
        }

        [TestMethod]
        public void LayerInstruction_MultipleRoomsFromStrings_IsCorrect()
        {
            var layer = LayerInstruction.FromStrings(CreateLines(
                "Layer: Size=(5,5)", "Stuff",
                "Room: (0,0)", "Floor: (1,1)", 
                "Room: (3,3)", "Floor: (4,4)"));
            
            Assert.AreEqual(2, layer.Rooms.Count);
            Assert.AreEqual(new XY(0, 0), layer.Rooms[0].Location);
            Assert.AreEqual(1, layer.Rooms[0].ObjectInstructions.Count);
            Assert.AreEqual(new XY(3, 3), layer.Rooms[1].Location);
            Assert.AreEqual(1, layer.Rooms[1].ObjectInstructions.Count);
        }

        [TestMethod]
        public void LayerInstruction_WithObjectLinkFromStrings_IsCorrect()
        {
            var layer = LayerInstruction.FromStrings(CreateLines(
                "Layer: Size=(5,5)", 
                "Room: (0,0)", "Floor: (1,1)", "Floor: (1,2)", "Link:(Floor,1,1)-(Floor,1,2)"));

            Assert.AreEqual(1, layer.Links.Count);
        }

        private List<string> CreateLines(params string[] args)
        {
            return args.ToList();
        }
    }
}
