using SecurityConsultantCore.Domain;
using SecurityConsultantCore.Domain.Basic;
using System.Collections.Generic;
using System.Linq;

namespace SecurityConsultantCore.Pathfinding
{
    public class PathBuilder
    {
        private readonly IPathFinder _pathFinder;
        private List<Path> _pathSegments = new List<Path>();
        private XYZ _currentNode;
        private bool isPathComplete = false;

        public PathBuilder(FacilityMap map, XYZ startingNode) : this(new CachedPathFinder(map), startingNode) {}
 
        public PathBuilder(IPathFinder pathFinder, XYZ startingNode)
        {
            _pathFinder = pathFinder;
            _currentNode = startingNode;

        }

        public void AddNode(XYZ nextNode)
        {
            if (nextNode.Equals(_currentNode))
                return;
            var path = _pathFinder.GetPath(_currentNode, nextNode);
            if (!path.IsValid)
                throw new InvalidPathException("No path found!");
            _pathSegments.Add(path);
            _currentNode = nextNode;
        }

        public IEnumerable<Path> Build()
        {
            if (!isPathComplete && _pathSegments.Any())
                RoutePathBackToOrigin();
            return _pathSegments;
        }

        private void RoutePathBackToOrigin()
        {
            var reversed = Reverse(_pathSegments);
            reversed.Add(new List<XYZ> { _pathSegments.First().Origin });
            _pathSegments.AddRange(CreatePathToOrigin(reversed));
            isPathComplete = true;
        }

        private List<IEnumerable<XYZ>> Reverse(IEnumerable<Path> segments)
        {
            return Enumerable.Reverse(segments.Select(x => Enumerable.Reverse(x))).ToList();
        }

        private IEnumerable<Path> CreatePathToOrigin(List<IEnumerable<XYZ>> nodes)
        {
            TrimDeadSegments(nodes, nodes.First().First());
            var result = new List<Path>();
            while (nodes.Any())
            {
                result.Add(new Path(nodes.First()));
                TrimDeadSegments(nodes, result.Last().Destination);
            }
            return result;
        }
        
        private void TrimDeadSegments(List<IEnumerable<XYZ>> segments, XYZ start)
        {
            var optionalNode = GetClosestNodeToOriginIndex(segments, start);
            if (optionalNode != -1)
                RemoveAllSegmentsThru(segments, optionalNode);
        }

        private int GetClosestNodeToOriginIndex(List<IEnumerable<XYZ>> segments, XYZ start)
        {
            return segments.FindLastIndex(x => x.Last().Equals(start));
        }

        private void RemoveAllSegmentsThru(List<IEnumerable<XYZ>> segments, int nodeIndex)
        {
            segments.RemoveRange(0, nodeIndex + 1);
        }
    }
}
