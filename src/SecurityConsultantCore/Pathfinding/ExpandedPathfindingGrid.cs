using System.Linq;
using SecurityConsultantCore.Domain;
using SecurityConsultantCore.Domain.Basic;

namespace SecurityConsultantCore.Pathfinding
{
    public class ExpandedPathfindingGrid
    {
        private readonly FacilityMap _map;
        private readonly int _layer;

        public ExpandedPathfindingGrid(FacilityMap map, int layer)
        {
            _map = map;
            _layer = layer;
        }

        public int this[int x, int y] => this[new XYZ(x, y, _layer)];
        public int this[XYZ xyz] => _map.IsOpen(xyz) || IsPortalConnection(xyz) ? 1 : 0;

        public double GetHeight()
        {
            return 1;
        }

        public double GetWidth()
        {
            return 1;
        }

        private bool IsPortalConnection(XYZ xyz)
        {
            return _map.Portals.Any(x => x.Obj.IsAtEndpoint(xyz));
        }
    }
}
