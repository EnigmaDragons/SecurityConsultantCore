using System.Collections.Generic;
using System.Linq;
using SecurityConsultantCore.Common;
using SecurityConsultantCore.Domain;
using SecurityConsultantCore.Domain.Basic;
using SecurityConsultantCore.Pathfinding;
using SecurityConsultantCore.FacilityObjects;

namespace SecurityConsultantCore.Thievery
{
    // TODO
    public class Thief : ObservedBase<IEnumerable<IValuable>>
    {
        private readonly IBody _thiefBody;
        private readonly IDesires _desires;
        private readonly FacilityMap _map;
        private readonly IPathFinder _pathFinder;

        private int ItemsRemaining { get; set; }
        private XYZ CurrentLocation { get; set; }
        private List<IValuable> StolenItems { get; } = new List<IValuable>();

        public Thief(IBody thiefBody, FacilityMap map) : this(thiefBody, map, new ThiefDesires(map.SpatialValuables)) { }

        public Thief(IBody thiefBody, FacilityMap map, IDesires desires) : this(thiefBody, map, desires, new CachedPathFinder(map), 1) { }

        public Thief(IBody thiefBody, FacilityMap map, IDesires desires, int itemCapacity) : this(thiefBody, map, desires, new CachedPathFinder(map), itemCapacity) { }

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

        private IEnumerable<SpatialValuable> GetTargets()
        {
            return _desires.Get();
        }

        private void StealAndKeepGoing(SpatialValuable valuable, Path path)
        {
            Steal(valuable);
            ItemsRemaining--;
            CurrentLocation = path.Last();
            Go();
        }

        private void Exit()
        {
            _thiefBody.BeginMove(GetExitPath(), () => _thiefBody.Exit());
            NotifySubscribers(StolenItems);
        }

        private Path GetExitPath()
        {
            var destinations = _map.Portals.Where(x => x.Obj.IsEdgePortal).Select(x => x.Location);
            foreach (var destination in destinations)
                if (_pathFinder.PathExists(CurrentLocation, destination))
                    return GetTrimmedPath(CurrentLocation, destination);
            return new Path();
        }

        private Path GetStealPath(SpatialValuable valuable)
        {
            var destinations = GetStealLocations(valuable).Shuffle();
            foreach (var destination in destinations)
                if (_pathFinder.PathExists(CurrentLocation, destination))
                    return GetTrimmedPath(CurrentLocation, destination);
            return new Path();
        }

        private IEnumerable<XYZ> GetStealLocations(SpatialValuable valuable)
        {
            return new List<XYZ>();
            // TODO
            //return GetDirectionsValuableCanBeStolenFrom(valuable)
            //    .Select(x => (XYZ)new XYZAdjacent(valuable.Location, x))
            //    .Where(x => _map.IsOpenSpace(x));
        }

        // TODO
        //private IEnumerable<Orientation> GetDirectionsValuableCanBeStolenFrom(SpatialValuable valuable)
        //{
        //    var space = _map[valuable.Location];
        //    if (IsFacilityValuable(valuable))
        //        return space.Contains("Wall") ? Lists.Of(valuable.Orientation) : Orientation.AllOrientations;
        //    var container = space.FacilityContainers.First(x => x.Valuables.Any(y => y.Equals(valuable.Obj)));
        //    return space.Contains("Wall") ? container.AccessibleFrom.Where(x => x.Equals(container.Orientation)) : container.AccessibleFrom;
        //}

        private Path GetTrimmedPath(XYZ start, XYZ end)
        {
            return new Path(_pathFinder.GetPath(start, end).Where(x => !x.Equals(SpecialLocation.OffOfMap)));
        }

        private void Steal(SpatialValuable valuable)
        {
            if (IsFacilityValuable(valuable))
                _thiefBody.StealAt(valuable);
            _map.Remove(valuable.Obj);
            StolenItems.Add(valuable.Obj);
        }

        private bool IsFacilityValuable(SpatialValuable valuable)
        {
            return true; // _map[valuable.Location].Contains(valuable.Obj.Type);
        }
    }
}
