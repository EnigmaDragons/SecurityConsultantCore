using SecurityConsultantCore.Domain;
using SecurityConsultantCore.Domain.Basic;
using System.Collections.Generic;
using System.Linq;

namespace SecurityConsultantCore.Pathfinding
{
    public class PatrolRouteProposal
    {
        private readonly IPathFinder _pathFinder;
        private readonly List<Path> _segments;
        private XYZ _currentNode;

        public PatrolRouteProposal(FacilityMap map, PatrolRoute route) : this(new CachedPathFinder(map), route.Start, route.Base.ToList()) {}

        public PatrolRouteProposal(FacilityMap map, XYZ startingNode) : this(new CachedPathFinder(map), startingNode, new List<Path>()) {}

        private PatrolRouteProposal(IPathFinder pathFinder, XYZ startingNode, List<Path> segments)
        {
            _pathFinder = pathFinder;
            _currentNode = startingNode;
            _segments = segments;
        }

        public void AddPathToDestination(XYZ nextNode)
        {
            if (nextNode.Equals(_currentNode))
                return;
            var path = _pathFinder.GetPath(_currentNode, nextNode);
            if (!path.IsValid)
                throw new InvalidPathException("No path found!");
            _segments.Add(path);
            _currentNode = nextNode;
        }

        public void Reset()
        {
            _currentNode = _segments.Count > 0 ? _segments.First().Origin : _currentNode;
            _segments.Clear();
        }

        public IRoute Finalize()
        {
            return new PatrolRoute(_segments);
        }
    }
}
