using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SecurityConsultantCore.Domain.Basic;

namespace SecurityConsultantCore.Pathfinding
{
    public class Path : IEnumerable<XYZ>
    {
        private readonly IEnumerable<XYZ> _path;
        public XYZ Origin => _path.First();
        public XYZ Destination => _path.Last();
        public bool IsValid { get; private set; }

        public Path() : this(new List<XYZ>(), false) {}

        public Path(params XYZ[] path) : this(path, true) { }

        public Path(IEnumerable<XYZ> path) : this(path, true) {}

        private Path(IEnumerable<XYZ> path, bool isValid)
        {
            IsValid = isValid;
            _path = path;
        }

        public IEnumerator<XYZ> GetEnumerator()
        {
            return _path.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void EnsurePathMatches(Path other)
        {
            if (other.Count() != this.Count())
                throw new NonMatchingException($"This path length of {this.Count()} did not match the other path's length of {other.Count()}");
            var otherPath = other.ToList();
            var thisPath = this.ToList();
            for (int nodeIndex = 0; nodeIndex < other.Count(); nodeIndex++)
                if (otherPath[nodeIndex] != thisPath[nodeIndex])
                    throw new NonMatchingException($"Node {nodeIndex}: {thisPath[nodeIndex]} does not equal {otherPath[nodeIndex]}");
        }
    }
}