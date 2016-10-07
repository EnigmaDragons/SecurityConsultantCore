using System.Linq;

namespace SecurityConsultantCore.Pathfinding
{
    public class RouteComparison
    {
        private readonly IRoute _route1;
        private readonly IRoute _route2;

        public RouteComparison(IRoute route1, IRoute route2)
        {
            _route1 = route1;
            _route2 = route2;
        }

        public void EnsureMatches()
        {
            if (_route1.Count() != _route2.Count())
                throw new NonMatchingException(string.Format("Route lengths were: {0}, {1}", _route1.Count(), _route2.Count()));
            var segments1 = _route1.ToList();
            var segments2 = _route2.ToList();
            for (int i = 0; i < segments1.Count(); i++)
                EnsurePathsMatch(segments1[i], segments2[i], i);
        }

        private void EnsurePathsMatch(Path path1, Path path2, int pathNumber)
        {
            try
            {
                path1.EnsurePathMatches(path2);
            }
            catch (NonMatchingException ex)
            {
                throw new NonMatchingException(string.Format("Path {0}: These paths do not match because {1}", pathNumber, ex.Message));
            }
        }
    }
}
