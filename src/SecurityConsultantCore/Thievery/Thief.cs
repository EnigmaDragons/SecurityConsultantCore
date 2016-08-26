using System;
using System.Collections.Generic;
using System.Linq;
using SecurityConsultantCore.Common;
using SecurityConsultantCore.Domain;
using SecurityConsultantCore.Domain.Basic;
using SecurityConsultantCore.MapGeneration;
using SecurityConsultantCore.Pathfinding;

namespace SecurityConsultantCore.Thievery
{
    public class Thief
    {
        private readonly FacilityMap _map;
        private readonly IPathFinder _pathFinder;

        private readonly Dictionary<int, Func<XYZ, XYZ>> _adjacentLocations = new Dictionary<int, Func<XYZ, XYZ>>
        {
            {0, xyz => xyz.Plus(new XYZ(0, -1, 0))},
            {90, xyz => xyz.Plus(new XYZ(1, 0, 0))},
            {180, xyz => xyz.Plus(new XYZ(0, 1, 0))},
            {270, xyz => xyz.Plus(new XYZ(-1, 0, 0))}
        };

        private XYZ _currentLocation = SpecialLocation.OffOfMap;
        private bool _isRobbing = true;
        private int _remainingItemCapacity;
        private XYZLocation<IValuable> _target;

        public Thief(FacilityMap map) : this(map, new CachedPathFinder(map), 1)
        {
        }

        public Thief(FacilityMap map, int itemCapacity) : this(map, new CachedPathFinder(map), itemCapacity)
        {
        }

        public Thief(FacilityMap map, IPathFinder pathFinder, int itemCapacity)
        {
            _map = map;
            _pathFinder = pathFinder;
            _remainingItemCapacity = itemCapacity;
        }

        public IEnumerable<ThiefInstruction> Instructions
        {
            get
            {
                if (!_map.Portals.Any(x => x.Obj.IsEdgePortal))
                    throw new MapException("No entrances to facility");
                while (_isRobbing)
                    yield return GetNextInstruction();
            }
        }

        private XYZLocation<IValuable> CurrentTarget => _target ?? (_target = _map.LocatedValuables.Shuffle().First());

        //TODO: make Task Completed method 
        public void TravelTo(XYZ location)
        {
            _currentLocation = location;
        }

        private ThiefInstruction GetNextInstruction()
        {
            if (_currentLocation.Equals(SpecialLocation.OffOfMap))
                return GetEnterInstruction();
            var path = GetStealPath();
            return path.IsValid ? GetStealInstruction(path) : GetExitInstruction();
        }

        private ThiefInstruction GetEnterInstruction()
        {
            var path = GetStealPath();
            var portal = path.IsValid
                ? GetPortalAt(path.First())
                : _map.Portals.Shuffle().First(x => x.Obj.IsEdgePortal);
            return new ThiefInstruction(Interactions.Enter, new XYZObjectLayer(portal),
                new Path(new List<XYZ> {portal.Location}));
        }

        private XYZLocation<FacilityPortal> GetPortalAt(XYZ location)
        {
            return _map.Portals.First(x => x.Obj.IsEdgePortal && x.Location.Equals(location));
        }

        private ThiefInstruction GetStealInstruction(Path path)
        {
            var instruction = new ThiefInstruction(Interactions.Steal, new XYZObjectLayer(CurrentTarget), path);
            //TODO: make taskCompleted
            _remainingItemCapacity--;
            return instruction;
        }

        private ThiefInstruction GetExitInstruction()
        {
            //TODO: make taskCompleted
            _isRobbing = false;
            var path = GetTrimmedPath(_currentLocation, SpecialLocation.OffOfMap);
            var portal = _map.Portals.First(x => x.Location.Equals(path.Last()) && x.Obj.IsEdgePortal);
            return new ThiefInstruction(Interactions.Exit, new XYZObjectLayer(portal), path);
        }

        private Path GetStealPath()
        {
            return new Path();
        }

        private IEnumerable<XYZ> GetStealLocations(FacilityMap map, XYZLocation<ValuableFacilityObject> valuable)
        {
            if (!map[valuable.Location].LowerObject.Type.Contains("Wall"))
                return
                    map.GetAdjacentLocations(valuable.Location).Where(x => x.Obj.IsOpenSpace()).Select(x => x.Location);
            var potentialLocation = _adjacentLocations[valuable.Obj.Orientation.Rotation].Invoke(valuable.Location);
            return map.IsOpenSpace(potentialLocation) ? new List<XYZ> {potentialLocation} : new List<XYZ>();
        }

        private Path GetTrimmedPath(XYZ start, XYZ end)
        {
            return new Path(_pathFinder.GetPath(start, end).Where(x => !x.Equals(SpecialLocation.OffOfMap)));
        }
    }
}