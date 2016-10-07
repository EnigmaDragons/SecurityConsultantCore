using System.Collections.Generic;
using System.Linq;
using SecurityConsultantCore.Pathfinding;

namespace SecurityConsultantCore.Security.Guards
{
    public class Guard
    {
        private readonly IGuardBody _guard;

        private PatrolRoute _patrolRoute = new PatrolRoute();
        private List<Path> _patrolSegments = new List<Path>();
        private bool _isDone;

        public Guard(IGuardBody guard)
        {
            _guard = guard;
        }

        public void AssignPatrolRoute(PatrolRoute route)
        {
            _patrolRoute = route;
            _patrolSegments = route.ToList();
        }

        public PatrolRoute WhatIsYourRoute()
        {
            return _patrolRoute;
        }

        public void Go()
        {
            if (_patrolRoute.Any())
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
            currentSegment = currentSegment + 1 == _patrolSegments.Count() ? currentSegment = 0 : currentSegment + 1;
            if (!_isDone)
                Patrol(currentSegment);
        }
    }
}
