using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SecurityConsultantCore.Domain.Basic;

namespace SecurityConsultantCore.Pathfinding
{
    public class Path : IEnumerable<XYZ>
    {
        private readonly IEnumerable<XYZ> _path;

        public Path()
        {
            IsValid = false;
            _path = new List<XYZ>();
        }

        public Path(IEnumerable<XYZ> path)
        {
            IsValid = true;
            _path = path.ToList();
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