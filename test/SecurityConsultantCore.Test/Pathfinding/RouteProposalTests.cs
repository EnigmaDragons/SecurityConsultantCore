using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTestExtensions;
using SecurityConsultantCore.Domain;
using SecurityConsultantCore.Domain.Basic;
using SecurityConsultantCore.MapGeneration;
using SecurityConsultantCore.Pathfinding;
using SecurityConsultantCore.Test.EngineMocks;
using System.Collections.Generic;
using System.Linq;

namespace SecurityConsultantCore.Test.Pathfinding
{
    [TestClass]
    public class PatrolRouteProposalTests
    {
        private readonly FacilityMap _map = new FacilityMap(new InMemoryWorld());
        private readonly FacilityObject _obstacle = new FacilityObject { ObjectLayer = ObjectLayer.LowerObject, Type = "Not none" };

        private FacilityLayer _layer;
        private PatrolRouteProposal _routeProposal;

        [TestInitialize]
        public void Init()
        {
            _routeProposal = new PatrolRouteProposal(_map, new XYZ(0, 0, 0), path => { });
            var builder = new LayerBuilder(3, 3);
            builder.PutFloor(new XY(0, 0), new XY(2, 2));
            _map.Add(_layer = builder.Build());
        }

        [TestMethod]
        public void PatrolRouteProposal_AddInvalidDestination_ExceptionThrown()
        {
            _layer[1, 1].Put(_obstacle);

            ExceptionAssert.Throws<InvalidPathException>(() => _routeProposal.AddPathToDestination(new XYZ(1, 1, 0)));
        }

        [TestMethod]
        public void PatrolRouteProposal_MakeLoopPath_PathTravelsInLoop()
        {
            _routeProposal.AddPathToDestination(new XYZ(2, 0, 0));
            _routeProposal.AddPathToDestination(new XYZ(2, 2, 0));
            _routeProposal.AddPathToDestination(new XYZ(0, 2, 0));
            _routeProposal.AddPathToDestination(new XYZ(0, 0, 0));

            var route = _routeProposal.Finalize();


            AssertRouteMatches(new List<XYZ> { new XYZ(2, 0, 0), new XYZ(2, 2, 0), new XYZ(0, 2, 0), new XYZ(0, 0, 0) }, route);
        }

        [TestMethod]
        public void PatrolRouteProposal_LoopWithBranchOffAtEnd_UsesCorrectPath()
        {
            _routeProposal.AddPathToDestination(new XYZ(2, 0, 0));
            _routeProposal.AddPathToDestination(new XYZ(2, 2, 0));
            _routeProposal.AddPathToDestination(new XYZ(0, 0, 0));
            _routeProposal.AddPathToDestination(new XYZ(0, 2, 0));

            var route = _routeProposal.Finalize();

            AssertRouteMatches(new List<XYZ> { new XYZ(2, 0, 0), new XYZ(2, 2, 0), new XYZ(0, 0, 0), new XYZ(0, 2, 0),
                new XYZ(0, 0, 0) }, route);
        }

        [TestMethod]
        public void PatrolRouteProposal_LoopWithoutStartPoint_UsesCorrectPath()
        {
            _routeProposal.AddPathToDestination(new XYZ(2, 0, 0));
            _routeProposal.AddPathToDestination(new XYZ(2, 2, 0));
            _routeProposal.AddPathToDestination(new XYZ(0, 2, 0));
            _routeProposal.AddPathToDestination(new XYZ(2, 0, 0));

            var route = _routeProposal.Finalize();

            AssertRouteMatches(new List<XYZ> { new XYZ(2, 0, 0), new XYZ(2, 2, 0), new XYZ(0, 2, 0), new XYZ(2, 0, 0),
                new XYZ(0, 0, 0) }, route);
        }

        [TestMethod]
        public void PatrolRouteProposal_LoopWithNeitherEndPointOrStartPoint_UsesCorrectPath()
        {
            _routeProposal.AddPathToDestination(new XYZ(1, 1, 0));
            _routeProposal.AddPathToDestination(new XYZ(2, 0, 0));
            _routeProposal.AddPathToDestination(new XYZ(2, 2, 0));
            _routeProposal.AddPathToDestination(new XYZ(1, 1, 0));
            _routeProposal.AddPathToDestination(new XYZ(0, 2, 0));

            var route = _routeProposal.Finalize();

            AssertRouteMatches(new List<XYZ> { new XYZ(1, 1, 0), new XYZ(2, 0, 0), new XYZ(2, 2, 0), new XYZ(1, 1, 0),
                new XYZ(0, 2, 0), new XYZ(1, 1, 0), new XYZ(0, 0, 0) }, route);
        }

        [TestMethod]
        public void PatrolRouteProposal_MultiLoopWithBranchesOffAndOnThoseLoops_EfficientlyResetsPath()
        {
            _routeProposal.AddPathToDestination(new XYZ(1, 0, 0));
            _routeProposal.AddPathToDestination(new XYZ(2, 0, 0));
            _routeProposal.AddPathToDestination(new XYZ(2, 1, 0));
            _routeProposal.AddPathToDestination(new XYZ(1, 0, 0));
            _routeProposal.AddPathToDestination(new XYZ(1, 1, 0));
            _routeProposal.AddPathToDestination(new XYZ(2, 2, 0));
            _routeProposal.AddPathToDestination(new XYZ(1, 2, 0));
            _routeProposal.AddPathToDestination(new XYZ(1, 1, 0));
            _routeProposal.AddPathToDestination(new XYZ(0, 2, 0));

            var route = _routeProposal.Finalize();

            AssertRouteMatches(new List<XYZ> { new XYZ(1, 0, 0), new XYZ(2, 0, 0), new XYZ(2, 1, 0), new XYZ(1, 0, 0),
                new XYZ(1, 1, 0), new XYZ(2, 2, 0), new XYZ(1, 2, 0), new XYZ(1, 1, 0),
                new XYZ(0, 2, 0), new XYZ(1, 1, 0), new XYZ(1, 0, 0), new XYZ(0, 0, 0) }, route);
        }

        [TestMethod]
        public void PatrolRouteProposal_ConstructedWithPatrolRoute_UsesRoute()
        {
            var oldRoute = new PatrolRoute(new Path(new XYZ(0, 1, 0)), new Path(new XYZ(0, 0, 0)));
            var proposal = new PatrolRouteProposal(_map, oldRoute, path => { });

            var newRoute = proposal.Finalize();

            new RouteComparison(oldRoute, newRoute).EnsureMatches();
        }

        [TestMethod]
        public void PatrolRouteProposal_FinalizeWithNoPointsAdded_OnlyStartingPoint()
        {
            var route = _routeProposal.Finalize();

            Assert.AreEqual(1, route.Count());
            Assert.AreEqual(new XYZ(0, 0, 0), route.Origin);
        }

        [TestMethod]
        public void PatrolRouteProposal_AddNodeToExistingRoute_SegmentAddedCorrectly()
        {
            var oldRoute = new PatrolRoute(new Path(new XYZ(0, 0, 0), new XYZ(0, 1, 0)));
            var proposal = new PatrolRouteProposal(_map, oldRoute, path => { });
            proposal.AddPathToDestination(new XYZ(0, 2, 0));

            var route = proposal.Finalize();

            AssertRouteOrginsMatch(new List<XYZ> { new XYZ(0, 0, 0), new XYZ(0, 1, 0), new XYZ(0, 2, 0),
                new XYZ(0, 1, 0) }, route);
        }

        private void AssertRouteMatches(List<XYZ> expectedPath, IRoute actualRoute)
        {
            var route = actualRoute.ToList();
            Assert.AreEqual(expectedPath.Count, route.Count());
            for (var i = 0; i < expectedPath.Count; i++)
                Assert.AreEqual(expectedPath[i], route[i].Last());
        }

        private void AssertRouteOrginsMatch(List<XYZ> expectedPath, IRoute actualRoute)
        {
            var route = actualRoute.ToList();
            Assert.AreEqual(expectedPath.Count, route.Count());
            for (var i = 0; i < expectedPath.Count; i++)
                Assert.AreEqual(expectedPath[i], route[i].First());
        }
    }
}
