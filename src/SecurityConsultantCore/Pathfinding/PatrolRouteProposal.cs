using SecurityConsultantCore.Domain;
using SecurityConsultantCore.Domain.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using SecurityConsultantCore.Common;

namespace SecurityConsultantCore.Pathfinding
{
    public class PatrolRouteProposal
    {
        private readonly IPathFinder _pathFinder;
        private readonly ObservableList<Path> _segments;
        private XYZ _currentNode;

        public PatrolRouteProposal(FacilityMap map, XYZ startingNode, PatrolRoute route, Action<IEnumerable<Path>> onChange) : 
            this(new CachedPathFinder(map), startingNode, new ObservableList<Path>(route.Route.ToList(), onChange)) {}

        public PatrolRouteProposal(FacilityMap map, XYZ startingNode, Action<IEnumerable<Path>> onChange) : 
            this(new CachedPathFinder(map), startingNode, new ObservableList<Path>(onChange)) {}

        private PatrolRouteProposal(IPathFinder pathFinder, XYZ startingNode, ObservableList<Path> segments)
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

        public PatrolRoute Finalize()
        {
            return new PatrolRoute(_segments);
        }
    }
}
