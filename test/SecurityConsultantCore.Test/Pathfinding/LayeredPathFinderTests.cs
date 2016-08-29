using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTestExtensions;
using SecurityConsultantCore.Domain;
using SecurityConsultantCore.Domain.Basic;
using SecurityConsultantCore.Pathfinding;

namespace SecurityConsultantCore.Test.Pathfinding
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class LayeredPathFinderTests
    {
        private readonly FacilityMap _map = new FacilityMap();
        private FacilityLayer _layer;
        private LayeredPathFinder _pathFinder;

        [TestInitialize]
        public void Init()
        {
            _layer = CreateFacilityLayer();
            _map.Add(_layer);
            _pathFinder = new LayeredPathFinder(_map);
        }

        [TestMethod]
        public void PathPlanner_StraightLine_ReturnsCorrectPath()
        {
            _layer.Put(2, 1, CreateFloor());

            var start = new XYZ(0, 1, 0);
            var end = new XYZ(2, 1, 0);
            var path = _pathFinder.GetPath(start, end);

            AssertValidPath(start, end, path);
        }

        [TestMethod]
        public void PathPlanner_FromOffMapToOnMap_ReturnsCorrectPath()
        {
            _layer.Put(0, 0, CreatePortal(new XYZ(0, 1, 0), SpecialLocation.OffOfMap));

            var start = SpecialLocation.OffOfMap;
            var end = new XYZ(2, 1, 0);
            var path = _pathFinder.GetPath(start, end);

            AssertValidPath(start, end, path);
        }

        [TestMethod]
        public void PathPlanner_PathToOffMap_ReturnsCorrectPath()
        {
            _layer.Put(0, 0, CreatePortal(new XYZ(0, 1, 0), SpecialLocation.OffOfMap));

            var start = new XYZ(2, 1, 0);
            var end = SpecialLocation.OffOfMap;
            var path = _pathFinder.GetPath(start, end);

            AssertValidPath(start, end, path);
        }

        [TestMethod]
        public void PathPlanner_InvalidPath_ThrowsException()
        {
            var start = SpecialLocation.OffOfMap;
            var end = new XYZ(1, 1, 0);

            ExceptionAssert.Throws<InvalidPathException>(() => _pathFinder.GetPath(start, end));
        }

        private FacilityLayer CreateFacilityLayer()
        {
            var layer = new FacilityLayer(3, 3);
            for (int x = 0; x < 3; x++)
                for (int y = 0; y < 3; y++)
                    layer.Put(x, y, CreateFloor());
            return layer;
        }

        private FacilitySpace CreateFloor()
        {
            return new FacilitySpace { Ground = new FacilityObject { Type = "Floor" } };
        }

        private FacilitySpace CreatePortal(XYZ endpoint1, XYZ endpoint2)
        {
            return new FacilitySpace { UpperObject = new FacilityPortal { Endpoint1 = endpoint1, Endpoint2 = endpoint2, ObjectLayer = ObjectLayer.UpperObject }};
        }

        private void AssertValidPath(XYZ start, XYZ end, Path path)
        {
            Assert.AreEqual(start, path.First());
            Assert.AreEqual(end, path.Last());
            var lastNode = path.First();
            foreach (var node in path)
            {
                if (node.Z == lastNode.Z && (Math.Abs(node.X - lastNode.X) > 1 || Math.Abs(node.Y - lastNode.Y) > 1))
                    Assert.IsTrue(false, "Node: " + node + " is not connected to Node: " + lastNode);
                lastNode = node;
            }
        }
    }
}
