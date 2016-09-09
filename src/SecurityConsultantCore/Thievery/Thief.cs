using System;
using System.Collections.Generic;
using System.Linq;
using SecurityConsultantCore.Common;
using SecurityConsultantCore.Domain;
using SecurityConsultantCore.Domain.Basic;
using SecurityConsultantCore.Pathfinding;

namespace SecurityConsultantCore.Thievery
{
    public class Thief
    {
        private readonly IBody _thiefBody;
        private readonly IDesires _desires;
        private readonly FacilityMap _map;
        private readonly IPathFinder _pathFinder;
        private readonly Dictionary<int, Func<XYZ, XYZ>> _adjacentLocations = new Dictionary<int, Func<XYZ, XYZ>>
        {
            { 0, xyz => xyz.Plus(new XYZ(0, -1, 0)) },
            { 90, xyz => xyz.Plus(new XYZ(1, 0, 0)) },
            { 180, xyz => xyz.Plus(new XYZ(0, 1, 0)) },
            { 270, xyz => xyz.Plus(new XYZ(-1, 0, 0)) }
        };

        private int ItemsRemaining { get; set; }
        private XYZ CurrentLocation { get; set; }

        public Thief(IBody thiefBody, FacilityMap map) : this(thiefBody, map, new ThiefDesires(map.LocatedValuables)) {}

        public Thief(IBody thiefBody, FacilityMap map, IDesires desires) : this(thiefBody, map, desires, new CachedPathFinder(map), 1) {}

        public Thief(IBody thiefBody, FacilityMap map, IDesires desires, int itemCapacity) : this(thiefBody, map, desires, new CachedPathFinder(map), itemCapacity) {}

        public Thief(IBody thiefBody, FacilityMap map, IDesires desires, IPathFinder pathFinder, int itemCapacity)
        {
            _thiefBody = thiefBody;
            _desires = desires;
            _map = map;
            _pathFinder = pathFinder;
            ItemsRemaining = itemCapacity;
            CurrentLocation = SpecialLocation.OffOfMap;
        }

        public void Go()
        {
            if (!GetTargets().Any() || ItemsRemaining == 0)
            {
                Exit();
                return;
            }

            var valuable = GetTargets().First();
            var path = GetStealPath(valuable);
            if (!path.IsValid)
            {
                Exit();
                return;
            }

            _thiefBody.BeginMove(path, () => StealAndKeepGoing(valuable, path));
        }

        private IEnumerable<LocatedValuable> GetTargets()
        {
            return _desires.Get();
        }

        private void StealAndKeepGoing(XYZLocation<IValuable> valuable, Path path)
        {
            Steal(valuable);
            ItemsRemaining--;
            CurrentLocation = path.Last();
            Go();
        }

        private void Exit()
        {
            _thiefBody.BeginMove(GetExitPath(), () => _thiefBody.Exit());
        }

        private Path GetExitPath()
        {
            var destinations = _map.Portals.Where(x => x.Obj.IsEdgePortal).Select(x => x.Location);
            foreach (var destination in destinations)
                if (_pathFinder.PathExists(CurrentLocation, destination))
                    return GetTrimmedPath(CurrentLocation, destination);
            return new Path();
        }

        private Path GetStealPath(XYZLocation<IValuable> valuable)
        {
            var destinations = GetStealLocations(valuable).Shuffle();
            foreach (var destination in destinations)
                if (_pathFinder.PathExists(CurrentLocation, destination))
                    return GetTrimmedPath(CurrentLocation, destination);
            return new Path();
        }

        private IEnumerable<XYZ> GetStealLocations(XYZLocation<IValuable> valuable)
        {
            return GetStealDirections(valuable)
                .Select(x => _adjacentLocations[x.Rotation].Invoke(valuable.Location))
                .Where(x => _map.Exists(x) && _map[x].IsOpenSpace());
        }

        private IEnumerable<Orientation> GetStealDirections(XYZLocation<IValuable> valuable)
        {
            var space = _map[valuable.Location];
            if (IsFacilityValuable(valuable))
                return space.Contains("Wall")
                    ? new List<Orientation> { space.Get(valuable.Obj.Type).Orientation }
                    : new List<Orientation> { Orientation.Up, Orientation.Right, Orientation.Down, Orientation.Left };
            var container = space.FacilityContainers.First(x => x.Valuables.Any(y => y.Equals(valuable.Obj)));
            return space.Contains("Wall") ? container.StealableOrientations.Where(x => x.Equals(container.Orientation)) : container.StealableOrientations;
        }

        private Path GetTrimmedPath(XYZ start, XYZ end)
        {
            return new Path(_pathFinder.GetPath(start, end).Where(x => !x.Equals(SpecialLocation.OffOfMap)));
        }

        private void Steal(XYZLocation<IValuable> valuable)
        {
            if (IsFacilityValuable(valuable))
                _thiefBody.StealAt(GetValuableTargetLocation(valuable));
            _map.Remove(valuable.Obj);
        }

        private bool IsFacilityValuable(XYZLocation<IValuable> valuable)
        {
            return _map[valuable.Location].Contains(valuable.Obj.Type);
        }

        private XYZObjectLayer GetValuableTargetLocation(XYZLocation<IValuable> valuable)
        {
            var layer = _map[valuable.Location].Get(valuable.Obj.Type).ObjectLayer;
            return new XYZObjectLayer(valuable.Location, layer);
        }
    }
}
