using System.Collections;
using System.Collections.Generic;

namespace SecurityConsultantCore.Pathfinding
{
    public class Route : IRoute
    {
        private IEnumerable<Path> _segments;

        public Route(params Path[] segments) : this((IEnumerable<Path>)segments) {}

        public Route(IEnumerable<Path> segments)
        {
            _segments = segments;
        }

        public IEnumerator<Path> GetEnumerator()
        {
            return _segments.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _segments.GetEnumerator();
        }
    }
}
