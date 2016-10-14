using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurityConsultantCore.Domain.Basic;
using SecurityConsultantCore.Pathfinding;
using SecurityConsultantCore.Security.Guards;
using SecurityConsultantCore.EventSystem;
using SecurityConsultantCore.EventSystem.Events;

namespace SecurityConsultantCore.Test.Security.Guards
{
    [TestClass]
    public class GuardTests : IGuardBody
    {
        private EventAggregator _eventAggregator;
        private XYZ _startLocation;
        private Guard _guard;
        private List<Path> _traversePaths = new List<Path>();
        private int _maxTravelSegments;

        [TestInitialize]
        public void Init()
        {
            _eventAggregator = new EventAggregator();
            _startLocation = new XYZ(0, 0, 0);
            _guard = new Guard(this, _startLocation, _eventAggregator);
        }

        [TestMethod]
        public void Guard_NoPath_GoesNowhere()
        {
            _guard.Go();

            AssertTraversedPath();
        }

        [TestMethod] 
        public void Guard_HasPatrolRoute_PatrolsRoute()
        {
            _maxTravelSegments = 4;
            _guard.AssignPatrolRoute(new PatrolRoute(new Path(new XYZ(0, 0, 0), new XYZ(0, 1, 0))));

            _guard.Go();

            AssertTraversedPath(new XYZ(0, 1, 0), new XYZ(0, 0, 0), new XYZ(0, 1, 0), new XYZ(0, 0, 0));
        }

        [TestMethod]
        public void Guard_WhatIsYourRoute_RouteIsSameAsAssigned()
        {
            var route = new PatrolRoute(new Path(new XYZ(0, 1, 0)));
            _guard.AssignPatrolRoute(route);

            var assignedRoute = _guard.WhatIsYourRoute();

            AssertRouteMatches(route.ToList(), assignedRoute.ToList());
        }

        [TestMethod]
        public void Guard_WhereAreYou_IsStartingLocation()
        {
            var currentLocation = new Guard(this, _startLocation, _eventAggregator).WhereAreYou();

            Assert.AreEqual(_startLocation, currentLocation);
        }

        [TestMethod]
        public void Guard_WhereAreYouAfterMoving_IsAtNewLocation()
        {
            _maxTravelSegments = 1;
            var route = new PatrolRoute(new Path(new XYZ(0, 1, 0)));
            _guard.AssignPatrolRoute(route);
            _guard.Go();

            var currentLocation = _guard.WhereAreYou();

            Assert.AreEqual(new XYZ(0, 1, 0), currentLocation);
        }

        [TestMethod]
        public void Gaurd_GameStartEvent_GoOccurs()
        {
            _maxTravelSegments = 1;
            var route = new PatrolRoute(new Path(new XYZ(0, 1, 0)));
            _guard.AssignPatrolRoute(route);
            _eventAggregator.Publish(new GameStartEvent());

            var currentLocation = _guard.WhereAreYou();

            Assert.AreEqual(new XYZ(0, 1, 0), currentLocation);
        }

        private void AssertRouteMatches(List<Path> expectedPath, List<Path> actualRoute)
        {
            for (int pathIndex = 0; pathIndex < expectedPath.Count; pathIndex++)
                expectedPath[pathIndex].EnsurePathMatches(actualRoute[pathIndex]);
        }

        private void AssertTraversedPath(params XYZ[] expectedPath)
        {
            Assert.AreEqual(_traversePaths.Count(), expectedPath.Count());
            for (var i = 0; i < expectedPath.Count(); i++)
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
