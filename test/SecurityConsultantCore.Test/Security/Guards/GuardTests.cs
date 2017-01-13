using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurityConsultantCore.Domain.Basic;
using SecurityConsultantCore.Engine;
using SecurityConsultantCore.Event;
using SecurityConsultantCore.Pathfinding;
using SecurityConsultantCore.Security.Guards;
using SecurityConsultantCore.Test._TestDoubles;

namespace SecurityConsultantCore.Test.Security.Guards
{
    [TestClass]
    public class GuardTests : IGuardBody
    {
        private static readonly XYZ _sampleDestination = new XYZ(2, 2, 2);
        private static readonly PatrolRoute _sampleRoute = new PatrolRoute(new Path(new XYZ(0, 1, 0), _sampleDestination));

        private Events _eventNotification;
        private XYZ _startLocation;
        private Guard _guard;
        private List<Path> _traversePaths = new List<Path>();
        private int _maxTravelSegments;

        private FakeEngineer _engineer = new FakeEngineer();

        [TestInitialize]
        public void Init()
        {
            _eventNotification = new Events();
            _startLocation = new XYZ(0, 0, 0);
            _guard = new Guard(this, _startLocation, _eventNotification);
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
            _guard.YourPatrolRouteIs(new PatrolRoute(new Path(new XYZ(0, 0, 0), new XYZ(0, 1, 0))));

            _guard.Go();

            AssertTraversedPath(new XYZ(0, 1, 0), new XYZ(0, 0, 0), new XYZ(0, 1, 0), new XYZ(0, 0, 0));
        }

        [TestMethod]
        public void Guard_WhatIsYourRoute_RouteIsSameAsAssigned()
        {
            _guard.YourPatrolRouteIs(_sampleRoute);

            var assignedRoute = _guard.WhatIsYourRoute();

            AssertRouteMatches(_sampleRoute.ToList(), assignedRoute.ToList());
        }

        [TestMethod]
        public void Guard_WhereAreYou_IsStartingLocation()
        {
            var currentLocation = new Guard(this, _startLocation, _eventNotification).WhereAreYou();

            Assert.AreEqual(_startLocation, currentLocation);
        }

        [TestMethod]
        public void Guard_WhereAreYouAfterMoving_IsAtNewLocation()
        {
            _maxTravelSegments = 1;
            _guard.YourPatrolRouteIs(_sampleRoute);
            _guard.Go();

            var currentLocation = _guard.WhereAreYou();

            Assert.AreEqual(_sampleDestination, currentLocation);
        }

        [TestMethod]
        public void Guard_GameStartEvent_GoOccurs()
        {
            _maxTravelSegments = 1;
            _guard.YourPatrolRouteIs(_sampleRoute);
            _eventNotification.Publish(new GameStartEvent());

            var currentLocation = _guard.WhereAreYou();

            Assert.AreEqual(_sampleDestination, currentLocation);
        }

        [TestMethod]
        public void Guard_ConsultsWithEngineer_EngineerKnowsGuard()
        {
            _guard.YourPatrolRouteIs(_sampleRoute);

            _guard.ConsultWith(_engineer);

            Assert.AreEqual(_guard, _engineer.ConversingWith);
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
