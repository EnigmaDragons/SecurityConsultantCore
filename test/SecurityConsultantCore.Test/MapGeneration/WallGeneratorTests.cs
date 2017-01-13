using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurityConsultantCore.Domain;
using SecurityConsultantCore.Domain.Basic;
using SecurityConsultantCore.Factories;
using SecurityConsultantCore.MapGeneration;
using SecurityConsultantCore.Test.Engine;

namespace SecurityConsultantCore.Test.MapGeneration
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class WallGeneratorTest
    {
        private WallGenerator _wallGenerator;
        private FacilityLayer _layer;
        private FacilitySpace _floorSpace;

        [TestInitialize]
        public void Init()
        {
            _floorSpace = new FacilitySpace { Ground = new FacilityObject { Type = FacilityObjectNames.Floor }};
            _wallGenerator = new WallGenerator();
        }

        [TestMethod]
        public void WallGenerator_EmptyMap_NoWalls()
        {
            var map = CreateEmptyMap();

            _wallGenerator.GenerateWalls(map);

            foreach (var space in map.SelectMany(x => x.Obj))
                Assert.IsFalse(IsWall(space));
        }

        [TestMethod]
        public void WallGenerator_Sides_AreCorrect()
        {
            _layer = new FacilityLayer(3, 3);
            PutFloor(1, 1);

            _wallGenerator.GenerateWalls(_layer);

            AssertWallCountIs(8);
            AssertWallEquals("6", 0, 1);
            AssertWallEquals("8", 1, 0);
            AssertWallEquals("4", 2, 1);
            AssertWallEquals("2", 1, 2);
        }

        [TestMethod]
        public void WallGenerator_InnerCorners_AreCorrect()
        {
            _layer = new FacilityLayer(3, 3);
            PutFloor(1, 1);

            _wallGenerator.GenerateWalls(_layer);

            AssertWallCountIs(8);
            AssertWallEquals("9", 0, 0);
            AssertWallEquals("7", 2, 0);
            AssertWallEquals("3", 0, 2);
            AssertWallEquals("1", 2, 2);
        }
        
        [TestMethod]
        public void WallGenerator_Pillar_IsCorrect()
        {
            _layer = new FacilityLayer(3, 3);
            PutFloorRow(0);
            PutFloor(0, 1);
            PutFloor(2, 1);
            PutFloorRow(2);

            _wallGenerator.GenerateWalls(_layer);

            AssertWallCountIs(1);
            AssertWallEquals("12346789", 1, 1);
        }

        [TestMethod]
        public void WallGenerator_Quad_IsCorrect()
        {
            _layer = new FacilityLayer(3, 3);
            PutFloor(0, 0);
            PutFloor(0, 2);
            PutFloor(2, 0);
            PutFloor(2, 2);

            _wallGenerator.GenerateWalls(_layer);

            AssertWallCountIs(5);
            AssertWallEquals("1379", 1, 1);
        }

        [TestMethod]
        public void WallGenerator_DoubleWallSidesAndCorners_AreCorrect()
        {
            _layer = new FacilityLayer(5, 5);
            PutFloorRow(0);
            PutFloor(0, 1);
            PutFloor(4, 1);
            PutFloor(0, 2);
            PutFloor(2, 2);
            PutFloor(4, 2);
            PutFloor(0, 3);
            PutFloor(4, 3);
            PutFloorRow(4);

            _wallGenerator.GenerateWalls(_layer);

            AssertWallCountIs(8);
            AssertWallEquals("123479", 1, 1);
            AssertWallEquals("1238", 2, 1);
            AssertWallEquals("123679", 3, 1);
            AssertWallEquals("3469", 3, 2);
            AssertWallEquals("136789", 3, 3);
            AssertWallEquals("2789", 2, 3);
            AssertWallEquals("134789", 1, 3);
            AssertWallEquals("1467", 1, 2);
        }

        [TestMethod]
        public void WallGenerator_DiagonalCorners_AreCorrect()
        {
            _layer = new FacilityLayer(6, 3);
            PutFloor(0, 2);
            PutFloor(2, 0);
            PutFloor(3, 0);
            PutFloor(5, 2);
            
            _wallGenerator.GenerateWalls(_layer);

            AssertWallCountIs(10);
            AssertWallEquals("37", 1, 1);
            AssertWallEquals("19", 4, 1);
        }

        [TestMethod]
        public void WallGenerator_DoubleWallEndsAndSideJunctions_AreCorrect()
        {
            _layer = new FacilityLayer(5, 5);
            PutFloor(1, 1);
            PutFloor(1, 3);
            PutFloor(2, 2);
            PutFloor(3, 1);
            PutFloor(3, 3);

            _wallGenerator.GenerateWalls(_layer);

            AssertWallCountIs(20);
            AssertWallEquals("79", 2, 0);
            AssertWallEquals("468", 2, 1);
            AssertWallEquals("39", 0, 2);
            AssertWallEquals("268", 1, 2);
            AssertWallEquals("13", 2, 4);
            AssertWallEquals("246", 2, 3);
            AssertWallEquals("17", 4, 2);
            AssertWallEquals("248", 3, 2);
        }

        [TestMethod]
        public void WallGenerator_TripleCorners_AreCorrect()
        {
            _layer = new FacilityLayer(5, 5);
            PutFloor(0, 2);
            PutFloor(2, 0);
            PutFloor(2, 2);
            PutFloor(2, 4);
            PutFloor(4, 2);

            _wallGenerator.GenerateWalls(_layer);

            AssertWallCountIs(16);
            AssertWallEquals("379", 1, 1);
            AssertWallEquals("179", 3, 1);
            AssertWallEquals("139", 1, 3);
            AssertWallEquals("137", 3, 3);
        }

        [TestMethod]
        public void WallGenerator_TripleSides_AreCorrect()
        {
            _layer = new FacilityLayer(5, 5);
            PutFloor(0, 0);
            PutFloor(0, 1);
            PutFloor(1, 0);
            PutFloor(0, 3);
            PutFloor(0, 4);
            PutFloor(1, 4);
            PutFloor(2, 2);
            PutFloor(3, 0);
            PutFloor(4, 0);
            PutFloor(4, 1);
            PutFloor(3, 4);
            PutFloor(4, 3);
            PutFloor(4, 4);

            _wallGenerator.GenerateWalls(_layer);

            AssertWallCountIs(12);
            AssertWallEquals("138", 2, 1);
            AssertWallEquals("167", 1, 2);
            AssertWallEquals("279", 2, 3);
            AssertWallEquals("349", 3, 2);
        }

        [TestMethod]
        public void WallGenerator_OuterCorners_AreCorrect()
        {
            _layer = new FacilityLayer(3, 3);
            PutFloorColumn(1);
            PutFloorRow(1);

            _wallGenerator.GenerateWalls(_layer);

            AssertWallCountIs(4);
            AssertWallEquals("68", 0, 0);
            AssertWallEquals("26", 0, 2);
            AssertWallEquals("48", 2, 0);
            AssertWallEquals("24", 2, 2);
        }

        [TestMethod]
        public void WallGenerator_SidePlusLeftCorners_AreCorrect()
        {
            _layer = new FacilityLayer(5, 5);
            PutFloor(0, 3);
            PutFloor(1, 0);
            PutFloor(2, 2);
            PutFloor(3, 4);
            PutFloor(4, 1);

            _wallGenerator.GenerateWalls(_layer);

            AssertWallCountIs(20);
            AssertWallEquals("18", 2, 1);
            AssertWallEquals("67", 1, 2);
            AssertWallEquals("34", 3, 2);
            AssertWallEquals("29", 2, 3);
        }

        [TestMethod]
        public void WallGenerator_SidePlusRightCorners_AreCorrect()
        {
            _layer = new FacilityLayer(5, 5);
            PutFloor(0, 1);
            PutFloor(1, 4);
            PutFloor(2, 2);
            PutFloor(3, 0);
            PutFloor(4, 3);

            _wallGenerator.GenerateWalls(_layer);

            AssertWallCountIs(20);
            AssertWallEquals("38", 2, 1);
            AssertWallEquals("16", 1, 2);
            AssertWallEquals("49", 3, 2);
            AssertWallEquals("27", 2, 3);
        }

        private void AssertWallCountIs(int expected)
        {
            Assert.AreEqual(expected, _layer.Where(IsWall).Count());
        }

        private void AssertWallEquals(string wallType, int x, int y)
        {
            AssertWallEquals(wallType, new XY(x, y));
        }

        private void AssertWallEquals(string wallType, XY location)
        {
            var expected = WallFactory.Create("Wall-" + wallType);
            var actual = _layer[location].LowerObject;
            Assert.AreEqual(expected, actual, 
                $"At [{location}] expected {expected.ToString()} but was {actual.ToString()}.");
        }

        private void PutFloor(int x, int y)
        {
            _layer.Put(new XY(x, y), _floorSpace);
        }

        private void PutFloorRow(int row)
        {
            for (int col = 0; col < _layer.Size.X; col++)
                _layer.Put(new XY(col, row), _floorSpace);
        }

        private void PutFloorColumn(int col)
        {
            for (int row = 0; row < _layer.Size.Y; row++)
                _layer.Put(new XY(col, row), _floorSpace);
        }

        private bool IsWall(XYLocation<FacilitySpace> space)
        {
            return space.Obj.Contains("Wall");
        }

        private FacilityMap CreateEmptyMap()
        {
            var map = new FacilityMap(new InMemoryWorld());
            _layer = new FacilityLayer(3, 3);
            map.Add(_layer);
            return map;
        }
    }
}
