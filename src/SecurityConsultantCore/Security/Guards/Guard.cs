using System.Collections.Generic;
using System.Linq;
using SecurityConsultantCore.Domain;
using SecurityConsultantCore.Domain.Basic;
using SecurityConsultantCore.Pathfinding;
using SecurityConsultantCore.Common;

namespace SecurityConsultantCore.Security.Guards
{
    public class Guard
    {
        private readonly IGuardBody _guard;
        private readonly PathBuilder _pathBuilder;

        private List<Path> _fullPath = new List<Path>();
        private bool _isDone;

        public Guard(IGuardBody guard, FacilityMap map, XYZ startLocation) : this(guard, new PathBuilder(map, startLocation)) { }

        public Guard(IGuardBody guard, PathBuilder pathBuilder)
        {
            _guard = guard;
            _pathBuilder = pathBuilder;
        }

        public void AddNextTravelPoint(XYZ point)
        {
            _pathBuilder.AddNode(point);
        }

        public void Go()
        {
            _fullPath = _pathBuilder.Build().ToList();
            if (_fullPath.Count > 0)
                Patrol(0);
        }

        public void GoHome()
        {
            _isDone = true;
        }

        private void Patrol(int currentSegment)
        {
            _guard.BeginMoving(_fullPath[currentSegment], 
                () => BeginWalkingNextSegmentIfNotDone(currentSegment));
        }

        private void BeginWalkingNextSegmentIfNotDone(int currentSegment)
        {
            currentSegment = currentSegment + 1 == _fullPath.Count ? currentSegment = 0 : currentSegment + 1;
            if (!_isDone)
                Patrol(currentSegment);
        }
    }
}
