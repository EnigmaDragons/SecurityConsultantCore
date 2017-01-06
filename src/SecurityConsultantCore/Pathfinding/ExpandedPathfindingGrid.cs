using System;
using System.Linq;
using SecurityConsultantCore.Domain;
using SecurityConsultantCore.Domain.Basic;

namespace SecurityConsultantCore.Pathfinding
{
    // TODO
    public class ExpandedPathfindingGrid
    {
        private readonly FacilityMap _map;

        public ExpandedPathfindingGrid(FacilityMap map)
        {
            _map = map;
        }

        public int this[int x, int y] => 1; //_layer[GetSpaceCoordinates(x, y)].IsOpenSpace || IsPortalConnection(x, y) ? 1 : 0;

        public double GetHeight()
        {
            return 1;
        }

        public double GetWidth()
        {
            return 1;
        }

        private bool IsPortalConnection(int x, int y)
        {
            var spacePosition = new XY(x % 3, y % 3);
            //foreach (var portal in _layer.Portals.Where(z => z.Location.Equals(GetSpaceCoordinates(x, y))))
            //    if (spacePosition.Equals(new XY(1, 1))
            //        || IsPortalEntrance(spacePosition, portal.Obj.Endpoint1, portal.Location)
            //        || IsPortalEntrance(spacePosition, portal.Obj.Endpoint2, portal.Location))
            //        return true;
            return false;
        }

        private XY GetSpaceCoordinates(int x, int y)
        {
            return new XY((int)Math.Floor((double)x / 3), (int)Math.Floor((double)y / 3));
        }

        private bool IsPortalEntrance(XY spacePosition, XY endpoint, XY portalLocation)
        {
            var connectPointX = endpoint.X - portalLocation.X + 1;
            var connectPointY = endpoint.Y - portalLocation.Y + 1;
            return spacePosition.Equals(new XY(connectPointX, connectPointY));
        }
    }
}
