using System;
using SecurityConsultantCore.Domain.Basic;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SecurityConsultantCore.Pathfinding
{
    public class PatrolRoute : IRoute
    {
        public IRoute Route { get; }
        public XYZ Origin => Route.First().Origin;

        public PatrolRoute(params Path[] route) : this(new Route(route)) { }

        public PatrolRoute(IEnumerable<Path> route) : this(new Route(route)) {}

        public PatrolRoute(IRoute route)
        {
            if (route.Count().Equals(0))
                throw new ArgumentException("route");
            Route = route;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<Path> GetEnumerator()
        {
            if (!Route.Any())
                return Route.GetEnumerator();
            var reversed = Reverse(Route);
            reversed.Add(new List<XYZ> { Route.First().Origin });
            return Route.Concat(CreatePathToOrigin(reversed)).GetEnumerator();
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

        public bool Matches(PatrolRoute route)
        {
            return route.Route.Count().Equals(Route.Count()) && route.Origin.Equals(Origin);
        }

        public bool IsStationary()
        {
            return Route.Count().Equals(1) 
                && Route.First().Origin.Equals(Route.First().Destination);
        }
    }
}
