using System.Collections.Generic;
using System.Linq;
using SecurityConsultantCore.Domain;
using SecurityConsultantCore.Pathfinding;
using SecurityConsultantCore.Domain.Basic;
using SecurityConsultantCore.EventSystem;
using SecurityConsultantCore.EventSystem.EventTypes;
using SecurityConsultantCore.PlayerCommands;

namespace SecurityConsultantCore.Security.Guards
{
    public class Guard : SecurityObjectBase
    {
        private readonly IGuardBody _guard;

        private XYZ _currentLocation;
        private PatrolRoute _patrolRoute;
        private List<Path> _patrolSegments = new List<Path>();
        private bool _isDone;

        public Guard(IGuardBody guard, XYZ startLocation, IEvents eventNotification)
        {
            _guard = guard;
            _currentLocation = startLocation;
            _patrolRoute = new PatrolRoute(new Path(startLocation));
            _patrolSegments.Add(new Path(startLocation));
            eventNotification.Subscribe<GameStartEvent>(start => Go());
        }

        public void YourPatrolRouteIs(PatrolRoute route)
        {
            _patrolRoute = route;
            _patrolSegments = route.ToList();
        }

        public XYZ WhereAreYou()
        {
            return _currentLocation;
        }

        public PatrolRoute WhatIsYourRoute()
        {
            return _patrolRoute;
        }

        public void Go()
        {
            if (_patrolRoute.Any() && !_patrolRoute.IsStationary())
                Patrol(0);
        }

        public void GoHome()
        {
            _isDone = true;
        }

        private void Patrol(int currentSegment)
        {
            _guard.BeginMoving(_patrolSegments[currentSegment], 
                () => BeginWalkingNextSegmentIfNotDone(currentSegment));
        }

        private void BeginWalkingNextSegmentIfNotDone(int currentSegment)
        {
            _currentLocation = _patrolSegments[currentSegment].Destination;
            currentSegment = currentSegment + 1 == _patrolSegments.Count() ? currentSegment = 0 : currentSegment + 1;
            if (!_isDone)
                Patrol(currentSegment);
        }

        public override void ConsultWith(IEngineer engineer)
        {
            engineer.IAm(this);
        }
    }
}
