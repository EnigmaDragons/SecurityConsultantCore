using System.Collections.Generic;
using SecurityConsultantCore.Domain.Basic;

namespace SecurityConsultantCore.Pathfinding
{
    public interface I2DPathFinder
    {
        void CancelSearch();
        List<PathFinderNode> BeginPathSearch(XY start, XY end);
    }
}