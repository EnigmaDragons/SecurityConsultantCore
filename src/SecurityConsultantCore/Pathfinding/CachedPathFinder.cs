using System.Collections.Generic;
using SecurityConsultantCore.Domain;
using SecurityConsultantCore.Domain.Basic;

namespace SecurityConsultantCore.Pathfinding
{
    public class CachedPathFinder : IPathFinder
    {
        private readonly Dictionary<Tuple<XYZ, XYZ>, Path> _cachedPaths = new Dictionary<Tuple<XYZ, XYZ>, Path>();
        private readonly LayeredPathFinder _pathFinder;

        public CachedPathFinder(FacilityMap map) : this(new LayeredPathFinder(map))
        {
        }

        public CachedPathFinder(LayeredPathFinder pathFinder)
        {
            _pathFinder = pathFinder;
        }

        public bool PathExists(XYZ start, XYZ end)
        {
            try
            {
                return GetPath(start, end).IsValid;
            }
            catch (InvalidPathException)
            {
                _cachedPaths.Add(new Tuple<XYZ, XYZ>(start, end), new Path());
                return false;
            }
        }

        public Path GetPath(XYZ start, XYZ end)
        {
            var key = new Tuple<XYZ, XYZ>(start, end);
            if (!_cachedPaths.ContainsKey(key))
                _cachedPaths.Add(key, _pathFinder.GetPath(start, end));
            return _cachedPaths[key];
        }
    }
}