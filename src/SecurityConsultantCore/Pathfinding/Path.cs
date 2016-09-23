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

        public Path() : this(new List<XYZ>(), false) {}

        public Path(IEnumerable<XYZ> path) : this(path, true) {}

        private Path(IEnumerable<XYZ> path, bool isValid)
        {
            IsValid = isValid;
            _path = path;
        }

        public bool IsValid { get; private set; }

        public IEnumerator<XYZ> GetEnumerator()
        {
            return _path.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


    }
}