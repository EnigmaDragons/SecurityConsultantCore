//using System.Diagnostics.CodeAnalysis;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using SecurityConsultantCore.Domain;
//using SecurityConsultantCore.Domain.Basic;
//using SecurityConsultantCore.Pathfinding;
//using SecurityConsultantCore.Test.EngineMocks;

//namespace SecurityConsultantCore.Test.Pathfinding
//{
//    // TODO: Fix these
//    [TestClass]
//    [ExcludeFromCodeCoverage]
//    public class ExpandedPathfindingGridTests
//    {
//        private FacilityMap _map;
//        private ExpandedPathfindingGrid _grid;

//        [TestMethod]
//        public void ExpandedPathfindingGrid_GetHeight_ReturnsCorrectValue()
//        {
//            AssertGetsCorrectHeight(3);
//            AssertGetsCorrectHeight(4);
//        }

//        [TestMethod]
//        public void ExpandedPathfindingGrid_GetWidth_ReturnsCorrectValue()
//        {
//            AssertGetsCorrectWidth(2);
//            AssertGetsCorrectWidth(3);
//        }

//        [TestMethod]
//        public void ExpandedPathfindingGrid_IndexClosedSpace_ReturnsZero()
//        {
//            InitGrid(1, 1);
//            var result = _grid[0, 0];
//            Assert.AreEqual(0, result);
//        }

//        [TestMethod]
//        public void ExpandedPathfindingGrid_IndexOpenSpace_ReturnsOne()
//        {
//            InitGrid(2, 2);
//            _map.Put(CreateFloor(), new XYZOrientation(1, 1, 0));

//            var result = _grid[4, 4];

//            Assert.AreEqual(1, result);
//        }

//        [TestMethod]
//        public void ExpandedPathfindingGrid_IndexPortalEnabledSpace_ReturnsOne()
//        {
//            InitGrid(3, 3);
//            _map.Put(CreateFloor(), new XYZOrientation(1, 0, 0));
//            _map.Put(CreateFloor(), new XYZOrientation(1, 2, 0));
//            _map.Put(new FacilityPortal { Endpoint1 = new XYZ(1, 0, 0), Endpoint2 = new XYZ(1, 2, 0) }, new XYZOrientation(1, 1, 0));

//            var result = _grid[4, 4];

//            Assert.AreEqual(1, result);
//        }

//        private FacilityObject CreateFloor()
//        {
//            return new StructureObject { Type = "Floor" };
//        }

//        private void AssertGetsCorrectHeight(int height)
//        {
//            InitGrid(0, height);
//            var result = _grid.GetHeight();
//            Assert.AreEqual(height * 3, result);
//        }

//        private void AssertGetsCorrectWidth(int width)
//        {
//            InitGrid(width, 0);
//            var result = _grid.GetWidth();
//            Assert.AreEqual(width * 3, result);
//        }

//        // This does not work with new Map
//        private void InitGrid(int width, int height)
//        {
//            _map = new FacilityMap(new InMemoryWorld());
//            _grid = new ExpandedPathfindingGrid(_map, 0);
//        }
//    }
//}
