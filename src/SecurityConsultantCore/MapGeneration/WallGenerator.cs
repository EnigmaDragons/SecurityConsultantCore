//using System.Collections.Generic;
//using System.Linq;
//using SecurityConsultantCore.Domain;
//using SecurityConsultantCore.Domain.Basic;
//using SecurityConsultantCore.Factories;
//using SecurityConsultantCore.Common;

//namespace SecurityConsultantCore.MapGeneration
//{
//    // TODO
//    public class WallGenerator
//    {
//        private static readonly Dictionary<XY, int> _offsetNeighborCoordinates = new Dictionary<XY, int>
//        {
//            {new XY(-1, -1), 1},
//            {new XY(-1, 0), 4},
//            {new XY(-1, 1), 7},
//            {new XY(0, -1), 2},
//            {new XY(1, -1), 3},
//            {new XY(1, 0), 6},
//            {new XY(0, 1), 8},
//            {new XY(1, 1), 9}
//        };

//        public void GenerateWalls(FacilityMap map)
//        {
//            var targets = map.WallSpaces;
//            targets.ForEach(x => 
//                map.Put(WallFactory.Create("Wall-" + GetWallType(new XY(x.X, x.Y)), x)) ;
//        }

//        private void PutWallSpace(FacilityMap map, XYZ location)
//        {
//        }

//        private List<int> GetNearbyFloorLocations(FacilityMap map, XY location)
//        {
//            var wallSpaces =
//            map.Floors.Select(floor => floor.Location.GetNeighbors())
//            .Except(map.Floors)
//            .Select(GetWallType)
//            .Distinct();

//            return layer.GetNeighbors(location)
//                .Where(IsFloor)
//                .Select(x => GetSpaceNumber(x.Location, location))
//                .OrderBy(x => x).ToList();
//        }

//        private string GetWallType(List<XY> floorLocations)
//        {
//            var wallType = "";
//            floorLocations.ForEach(x => wallType += x);
//            return wallType;
//        }

//        private int GetSpaceNumber(XY spaceLocation, XY wallLocation)
//        {
//            var offset = wallLocation.GetOffset(spaceLocation);
//            return _offsetNeighborCoordinates[offset];
//        }
//    }
//}