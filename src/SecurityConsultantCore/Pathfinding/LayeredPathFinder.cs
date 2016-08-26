using System;
using System.Collections.Generic;
using System.Linq;
using SecurityConsultantCore.Domain;
using SecurityConsultantCore.Domain.Basic;

namespace SecurityConsultantCore.Pathfinding
{
    public class LayeredPathFinder
    {
        private readonly FacilityMap _map;
        private readonly List<Smart2DPathFinder> _layerPathFinders = new List<Smart2DPathFinder>();

        public LayeredPathFinder(FacilityMap map)
        {
            _map = map;
        }

        private List<Smart2DPathFinder> LayerPathFinders
        {
            get
            {
                if (_layerPathFinders.Count == 0)
                    for (var i = 0; i < _map.LayerCount; i++)
                        _layerPathFinders.Add(new Smart2DPathFinder(_map[i]));
                return _layerPathFinders;
            }
        }

        public Path GetPath(XYZ start, XYZ end)
        {
            try
            {
                return FindPath(start, end);
            }
            catch (Exception ex)
            {
                throw new InvalidPathException("No path could be found from " + start + " to " + end, ex);
            }
        }

        private Path FindPath(XYZ start, XYZ end)
        {
            var startsOffMap = start.Equals(SpecialLocation.OffOfMap);
            var endsOffMap = end.Equals(SpecialLocation.OffOfMap);
            var onMapStartLocations = startsOffMap ? GetOnMapEndpoints() : new List<XYZ> {start};
            var onMapEndLocations = endsOffMap ? GetOnMapEndpoints() : new List<XYZ> {end};
            var path = FindPath(onMapStartLocations, onMapEndLocations);
            if (startsOffMap)
                path.Insert(0, SpecialLocation.OffOfMap);
            if (endsOffMap)
                path.Add(end);
            return new Path(path);
        }

        private IEnumerable<XYZ> GetOnMapEndpoints()
        {
            var relevantPortals = _map.Portals.Where(x => x.Obj.IsEdgePortal).Select(x => x.Obj);
            return relevantPortals.Select(x => x.Endpoint1.Equals(SpecialLocation.OffOfMap) ? x.Endpoint2 : x.Endpoint1);
        }

        private List<XYZ> FindPath(IEnumerable<XYZ> onMapStartLocations, IEnumerable<XYZ> onMapEndLocations)
        {
            var path = new List<XYZ>();
            foreach (var startLocation in onMapStartLocations)
                foreach (var endLocation in onMapEndLocations)
                    try
                    {
                        path =
                            LayerPathFinders[startLocation.Z].BeginPathSearch(startLocation, endLocation)
                                .Select(x => new XYZ(x, startLocation.Z))
                                .ToList();
                    }
                    catch (Exception)
                    {
                    }
            if (!path.Any())
                throw new InvalidPathException();
            return path;
        }
    }
}