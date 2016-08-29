using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurityConsultantCore.Domain;
using SecurityConsultantCore.Domain.Basic;
using SecurityConsultantCore.Pathfinding;

namespace SecurityConsultantCore.Test.Pathfinding
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ExpandedPathfindingGridTests
    {
        private FacilityLayer _layer;
        private ExpandedPathfindingGrid _grid;

        [TestMethod]
        public void PathfindingGrid_GetHeight_ReturnsCorrectValue()
        {
            AssertGetsCorrectHeight(3);
            AssertGetsCorrectHeight(4);
        }

        [TestMethod]
        public void PathfindingGrid_GetWidth_ReturnsCorrectValue()
        {
            AssertGetsCorrectWidth(2);
            AssertGetsCorrectWidth(3);
        }

        [TestMethod]
        public void PathfindingGrid_IndexClosedSpace_ReturnsZero()
        {
            InitGrid(1, 1);
            var result = _grid[0, 0];
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void PathfindingGrid_IndexOpenSpace_ReturnsOne()
        {
            InitGrid(2, 2);
            _layer.Put(1, 1, CreateFloor());

            var result = _grid[4, 4];

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void PathfindingGrid_IndexPortalEnabledSpace_ReturnsOne()
        {
            InitGrid(3, 3);
            _layer.Put(1, 0, CreateFloor());
            _layer.Put(1, 2, CreateFloor());
            _layer.Put(1, 1, new FacilitySpace { UpperObject = new FacilityPortal { Endpoint1 = new XYZ(1, 0, 0), Endpoint2 = new XYZ(1, 2, 0) }});

            var result = _grid[4, 4];

            Assert.AreEqual(1, result);
        }

        private void InitGrid(int width, int height)
        {
            _layer = new FacilityLayer(width, height);
            _grid = new ExpandedPathfindingGrid(_layer);
        }

        private FacilitySpace CreateFloor()
        {
            return new FacilitySpace {Ground = new FacilityObject {Type = "Floor"}};
        }

        private void AssertGetsCorrectHeight(int height)
        {
            InitGrid(0, height);
            var result = _grid.GetHeight();
            Assert.AreEqual(height * 3, result);
        }

        private void AssertGetsCorrectWidth(int width)
        {
            InitGrid(width, 0);
            var result = _grid.GetWidth();
            Assert.AreEqual(width * 3, result);
        }
    }
}
