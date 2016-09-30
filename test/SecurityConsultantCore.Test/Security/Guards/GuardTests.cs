﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTestExtensions;
using SecurityConsultantCore.Domain;
using SecurityConsultantCore.Domain.Basic;
using SecurityConsultantCore.MapGeneration;
using SecurityConsultantCore.Pathfinding;
using SecurityConsultantCore.Security.Guards;

namespace SecurityConsultantCore.Test.Security.Guards
{
    [TestClass]
    public class GuardTests : IGuardBody
    {
        private readonly FacilityMap _map = new FacilityMap();
        private readonly FacilityObject _obstacle = new FacilityObject { ObjectLayer = ObjectLayer.LowerObject, Type = "Not none" };

        private FacilityLayer _layer;
        private Guard _guard;

        private readonly List<Path> _traversePaths = new List<Path>();
        private int _maxTravelSegments;

        [TestInitialize]
        public void Init()
        {
            _guard = new Guard(this, _map, new XYZ(0, 0, 0));
            var builder = new LayerBuilder(3, 3);
            builder.PutFloor(new XY(0, 0), new XY(2, 2));
            _map.Add(_layer = builder.Build());
        }

        [TestMethod]
        public void Guard_GoWithOnePoint_TraverseToPoint()
        {
            SetMaxTravelSegments(1);
            _guard.AddNextTravelPoint(new XYZ(1, 1, 0));

            _guard.Go();

            AssertTraversedPath(new List<XYZ> { new XYZ(1, 1, 0) });
        }

        [TestMethod]
        public void Guard_PathLoopedUponGo_TraverseFullLoopTwice()
        {
            _maxTravelSegments = 4;
            _guard.AddNextTravelPoint(new XYZ(1, 1, 0));

            _guard.Go();

            AssertTraversedPath(new List<XYZ> { new XYZ(1, 1, 0), new XYZ(0, 0, 0), new XYZ(1, 1, 0), new XYZ(0, 0, 0) });
        }

        [TestMethod]
        public void Guard_NoPath_GoesNowhere()
        {
            _maxTravelSegments = 1;

            _guard.Go();

            AssertTraversedPath(new List<XYZ>());
        }

        [TestMethod]
        public void Guard_AddInvalidDestination_ExceptionThrown()
        {
            _layer[1, 1].Put(_obstacle);

            ExceptionAssert.Throws<InvalidPathException>(() => _guard.AddNextTravelPoint(new XYZ(1, 1, 0)));
        }

        [TestMethod]
        public void Guard_MakeLoopPath_PathTravelsInLoop()
        {
            _maxTravelSegments = 8;
            _guard.AddNextTravelPoint(new XYZ(2, 0, 0));
            _guard.AddNextTravelPoint(new XYZ(2, 2, 0));
            _guard.AddNextTravelPoint(new XYZ(0, 2, 0));
            _guard.AddNextTravelPoint(new XYZ(0, 0, 0));

            _guard.Go();

            AssertTraversedPath(new List<XYZ> { new XYZ(2, 0, 0), new XYZ(2, 2, 0), new XYZ(0, 2, 0), new XYZ(0, 0, 0),
                new XYZ(2, 0, 0), new XYZ(2, 2, 0), new XYZ(0, 2, 0), new XYZ(0, 0, 0) });
        }

        [TestMethod]
        public void Guard_LoopWithBranchOffAtEnd_UsesCorrectPath()
        {
            _maxTravelSegments = 8;
            _guard.AddNextTravelPoint(new XYZ(2, 0, 0));
            _guard.AddNextTravelPoint(new XYZ(2, 2, 0));
            _guard.AddNextTravelPoint(new XYZ(0, 0, 0));
            _guard.AddNextTravelPoint(new XYZ(0, 2, 0));

            _guard.Go();

            AssertTraversedPath(new List<XYZ> { new XYZ(2, 0, 0), new XYZ(2, 2, 0), new XYZ(0, 0, 0), new XYZ(0, 2, 0),
                new XYZ(0, 0, 0), new XYZ(2, 0, 0), new XYZ(2, 2, 0), new XYZ(0, 0, 0) });
        }

        [TestMethod]
        public void Guard_LoopWithoutStartPoint_UsesCorrectPath()
        {
            _maxTravelSegments = 8;
            _guard.AddNextTravelPoint(new XYZ(2, 0, 0));
            _guard.AddNextTravelPoint(new XYZ(2, 2, 0));
            _guard.AddNextTravelPoint(new XYZ(0, 2, 0));
            _guard.AddNextTravelPoint(new XYZ(2, 0, 0));

            _guard.Go();

            AssertTraversedPath(new List<XYZ> { new XYZ(2, 0, 0), new XYZ(2, 2, 0), new XYZ(0, 2, 0), new XYZ(2, 0, 0),
                new XYZ(0, 0, 0), new XYZ(2, 0, 0), new XYZ(2, 2, 0), new XYZ(0, 2, 0) });
        }

        [TestMethod]
        public void Guard_LoopWithNeitherEndPointOrStartPoint_UsesCorrectPath()
        {
            _maxTravelSegments = 8;
            _guard.AddNextTravelPoint(new XYZ(1, 1, 0));
            _guard.AddNextTravelPoint(new XYZ(2, 0, 0));
            _guard.AddNextTravelPoint(new XYZ(2, 2, 0));
            _guard.AddNextTravelPoint(new XYZ(1, 1, 0));
            _guard.AddNextTravelPoint(new XYZ(0, 2, 0));

            _guard.Go();

            AssertTraversedPath(new List<XYZ> { new XYZ(1, 1, 0), new XYZ(2, 0, 0), new XYZ(2, 2, 0), new XYZ(1, 1, 0),
                new XYZ(0, 2, 0), new XYZ(1, 1, 0), new XYZ(0, 0, 0), new XYZ(1, 1, 0) });
        }

        [TestMethod]
        public void Guard_MultiLoopWithBranchesOffAndOnThoseLoops_EfficientlyResetsPath()
        {
            _maxTravelSegments = 12;
            _guard.AddNextTravelPoint(new XYZ(1, 0, 0));
            _guard.AddNextTravelPoint(new XYZ(2, 0, 0));
            _guard.AddNextTravelPoint(new XYZ(2, 1, 0));
            _guard.AddNextTravelPoint(new XYZ(1, 0, 0));
            _guard.AddNextTravelPoint(new XYZ(1, 1, 0));
            _guard.AddNextTravelPoint(new XYZ(2, 2, 0));
            _guard.AddNextTravelPoint(new XYZ(1, 2, 0));
            _guard.AddNextTravelPoint(new XYZ(1, 1, 0));
            _guard.AddNextTravelPoint(new XYZ(0, 2, 0));

            _guard.Go();

            AssertTraversedPath(new List<XYZ> { new XYZ(1, 0, 0), new XYZ(2, 0, 0), new XYZ(2, 1, 0), new XYZ(1, 0, 0),
                new XYZ(1, 1, 0), new XYZ(2, 2, 0), new XYZ(1, 2, 0), new XYZ(1, 1, 0),
                new XYZ(0, 2, 0), new XYZ(1, 1, 0), new XYZ(1, 0, 0), new XYZ(0, 0, 0) });
        }

        private void SetMaxTravelSegments(int nPaths)
        {
            _maxTravelSegments = nPaths;
        }

        private void AssertTraversedPath(List<XYZ> expectedPath)
        {
            Assert.IsTrue(_traversePaths.Count == expectedPath.Count);
            for (var i = 0; i < expectedPath.Count; i++)
                Assert.AreEqual(expectedPath[i], _traversePaths[i].Last());
        }

        public void BeginMoving(Path path, Action callBack)
        {
            _traversePaths.Add(path);
            if (_maxTravelSegments == 1)
                _guard.GoHome();
            else
                _maxTravelSegments--;
            callBack.Invoke();
        }
    }
}
