using System;
using System.Collections.Generic;
using System.Linq;
using SecurityConsultantCore.Domain;
using SecurityConsultantCore.Domain.Basic;

namespace SecurityConsultantCore.Pathfinding
{
    //TODO: rename
    public class Smart2DPathFinder
    {
        private readonly I2DPathFinder _pathFinder;

        public Smart2DPathFinder(FacilityLayer layer) : this(new TwoDPathFinder(new ExpandedPathfindingGrid(layer)))
        {
        }

        public Smart2DPathFinder(I2DPathFinder pathFinder)
        {
            _pathFinder = pathFinder;
        }

        public void CancelSearch()
        {
            _pathFinder.CancelSearch();
        }

        public List<XY> BeginPathSearch(XY start, XY end)
        {
            return
                Contract(
                    _pathFinder.BeginPathSearch(GetExpandedTarget(start), GetExpandedTarget(end))
                        .Select(node => new XY(node.X, node.Y)));
        }

        private List<XY> Contract(IEnumerable<XY> zoomedInList)
        {
            var result = new List<XY>();
            foreach (var node in zoomedInList)
            {
                var position = new XY(GetContractedCoordinate(node.X), GetContractedCoordinate(node.Y));
                if ((result.Count == 0) || !result.Last().Equals(position))
                    result.Add(position);
            }
            return result;
        }

        private XY GetExpandedTarget(XY start)
        {
            return new XY(start.X*3 + 1, start.Y*3 + 1);
        }

        private int GetContractedCoordinate(int x)
        {
            return (int) Math.Floor((double) x/3);
        }
    }
}