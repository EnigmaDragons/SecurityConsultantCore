//using System.Collections.Generic;
//using System.Diagnostics.CodeAnalysis;
//using System.Linq;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using SecurityConsultantCore.Domain;
//using SecurityConsultantCore.Domain.Basic;
//using SecurityConsultantCore.MapGeneration;

//namespace SecurityConsultantCore.Test.MapGeneration
//{
//    [TestClass]
//    [ExcludeFromCodeCoverage]
//    public class LayerBuilderTests
//    {
//        [TestMethod]
//        public void LayerBuilder_CreateEmptyLayer_LayerCreated()
//        {
//            var builder = new LayerBuilder(10, 10);

//            var layer = builder.Build();

//            Assert.AreEqual(new XY(10, 10), layer.Size);
//        }

//        [TestMethod]
//        public void LayerBuilder_PutSingleFloorSpace_FloorCorrectlyAdded()
//        {
//            var builder = new LayerBuilder(3, 3);

//            builder.PutFloor(1, 1);
//            var layer = builder.Build();

//            AssertContains(1, 1, layer, FacilityObjectNames.Floor);
//        }

//        [TestMethod]
//        public void LayerBuilder_PutFloorRectangle_FloorCorrectlyAdded()
//        {
//            var builder = new LayerBuilder(3, 3);

//            builder.PutFloor(new XY(0, 0), new XY(2, 2));
//            var layer = builder.Build();

//            AssertContains(new XY(0, 0), new XY(2, 2), layer, FacilityObjectNames.Floor);
//        }

//        [TestMethod]
//        public void LayerBuilder_OnBuild_WallsCorrectlyAdded()
//        {
//            var builder = new LayerBuilder(3, 3);

//            builder.PutFloor(1, 1);
//            var layer = builder.Build();

//            AssertContains(new XY(0, 0), new XY(0, 2), layer, "Wall");
//            AssertContains(new XY(0, 0), new XY(2, 0), layer, "Wall");
//            AssertContains(new XY(2, 0), new XY(2, 2), layer, "Wall");
//            AssertContains(new XY(0, 2), new XY(2, 2), layer, "Wall");
//        }

//        [TestMethod]
//        public void LayerBuilder_PutLowerObject_ObjectPutCorrectly()
//        {
//            var builder = new LayerBuilder(3, 3);
//            var cash = new FacilityObject { Type = "Cash", Orientation = Orientation.Up, ObjectLayer = ObjectLayer.LowerObject };

//            builder.PutFloor(1, 1);
//            builder.Put(1, 1, cash);
//            var layer = builder.Build();

//            Assert.IsTrue(layer[1, 1].Contains(cash));
//            Assert.AreEqual(cash, layer[1, 1].LowerObject);
//        }

//        [TestMethod]
//        public void LayerBuilder_PutUpperObject_ObjectPutCorrectly()
//        {
//            var builder = new LayerBuilder(3, 3);
//            var painting = new FacilityObject { Type = "Painting", Orientation = Orientation.Right, ObjectLayer = ObjectLayer.UpperObject };

//            builder.PutFloor(1, 1);
//            builder.Put(0, 1, painting);
//            var layer = builder.Build();

//            Assert.IsTrue(layer[0, 1].Contains(painting));
//            Assert.AreEqual(painting, layer[0, 1].UpperObject);
//        }

//        [TestMethod]
//        public void LayerBuilder_PutFloor_FloorWithCorrectObjectLayer()
//        {
//            var builder = new LayerBuilder(3, 3);

//            builder.PutFloor(1, 1);
//            var layer = builder.Build();

//            Assert.IsTrue(layer[1, 1].Ground.ObjectLayer == ObjectLayer.Ground);
//        }

//        [TestMethod]
//        public void LayerAssembler_PutObjectFromInstruction_LayerCorrect()
//        {
//            var inst = ObjectInstruction.FromString("Floor:(1,1,R)");
//            var builder = new LayerBuilder(3, 3);
//            builder.Put(inst[0]);

//            var layer = builder.Build();

//            Assert.AreEqual("Floor", layer[1, 1].Ground.Type);
//            Assert.AreEqual(Orientation.Right, layer[1, 1].Ground.Orientation);
//        }

//        [TestMethod]
//        public void LayerAssembler_AssembleSimpleLayer_LayerCorrect()
//        {
//            var instructions = LayerInstruction.FromStrings(CreateLines("Layer:Size=(3,3)", "Room: (0,0)", "Floor:(1,1)"));

//            var layer = LayerBuilder.Assemble(instructions);

//            Assert.AreEqual("Floor", layer[1, 1].Ground.Type);
//        }

//        [TestMethod]
//        public void LayerAssembler_AssembleSimpleLayerWithRoomOffset_LayerCorrect()
//        {
//            var instructions = LayerInstruction.FromStrings(CreateLines("Layer:Size=(5,5)", "Room:(2,2)", "Floor:(1,1)"));

//            var layer = LayerBuilder.Assemble(instructions);

//            Assert.AreEqual("Floor", layer[3, 3].Ground.Type);
//        }

//        [TestMethod]
//        public void LayerAssembler_AssembleSimpleLayerWithObjectLink_LayerCorrect()
//        {
//            var instructions = LayerInstruction.FromStrings(CreateLines("Layer:Size=(3,3)", 
//                "Room: (0,0)", "Table:(1,1)", "Table:(1,2)", "Link:(Table,1,1)-(Table,1,2)"));

//            var layer = LayerBuilder.Assemble(instructions);

//            Assert.AreEqual(1, layer[1,1].LowerObject.LinkedObjs.Count);
//            Assert.AreEqual(1, layer[1,2].LowerObject.LinkedObjs.Count);
//        }

//        private List<string> CreateLines(params string[] args)
//        {
//            return args.ToList();
//        }

//        private void AssertContains(int x, int y, FacilityLayer layer, string type)
//        {
//            Assert.IsTrue(layer[x, y].Contains(type), $"At [{x}, {y}] expected: {type}.");
//        }

//        private void AssertContains(XY start, XY end, FacilityLayer layer, string type)
//        {
//            new XYRange(start, end).ToList().ForEach(x => AssertContains(x.X, x.Y, layer, type));
//        }
//    }
//}
