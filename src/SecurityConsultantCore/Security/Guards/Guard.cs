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
        private readonly List<Path> _paths = new List<Path>();
        private readonly IGuardBody _guard;
        private readonly IPathFinder _pathFinder;
        private readonly XYZ _startLocation;

        private XYZ _location;
        private int _pathIndex = 0;
        private bool _isDisposed;

        public Guard(IGuardBody guard, FacilityMap map, XYZ startLocation) : this(guard, new CachedPathFinder(map), startLocation) { }

        public Guard(IGuardBody guard, IPathFinder pathFinder, XYZ startLocation)
        {
            _guard = guard;
            _pathFinder = pathFinder;
            _location = startLocation;
            _startLocation = startLocation;
        }

        public void AddNextTravelPoint(XYZ point)
        {
            if (point.Equals(_location))
                throw new InvalidPathException("So you want me to stand here!");
            var path = _pathFinder.GetPath(_location, point);
            if (!path.IsValid)
                throw new InvalidPathException("I can't go there!");
            _paths.Add(path);
            _location = path.Last();
        }

        public void Go()
        {
            if (_paths.Count == 0)
                return;
            LoopPath();
            Patrol();
        }

        private void Patrol()
        {
            _guard.BeginMoving(_paths[_pathIndex], () =>
            {
                _pathIndex = _pathIndex + 1 == _paths.Count ? _pathIndex = 0 : _pathIndex + 1;
                if (!_isDisposed)
                    Patrol();
            });
        }

        public void Dispose()
        {
            _isDisposed = true;
        }

        private void LoopPath()
        {

        }
    }
}
