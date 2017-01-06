//using System.Collections.Generic;
//using System.Linq;
//using SecurityConsultantCore.Domain;
//using SecurityConsultantCore.Domain.Basic;
//using SecurityConsultantCore.Factories;

//namespace SecurityConsultantCore.MapGeneration
//{
// TODO
//    public class WallGenerator
//    {
//        private static readonly Dictionary<XY, int> _offsetNeightborCoordinates = new Dictionary<XY, int>
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
//            foreach (var layer in map)
//                GenerateWalls(layer.Obj);
//        }

//        public void GenerateWalls(FacilityLayer layer)
//        {
//            GetWallLocations(layer).ForEach(x => PutWallSpace(layer, x));
//        }

//        private List<XY> GetWallLocations(FacilityLayer layer)
//        {
//            return new List<XY>();
//            //return layer.Where(IsFloor)
//            //    .SelectMany(y => layer.GetNeighbors(y.Location).Where(x => !IsFloor(x)))
//            //    .Select(z => z.Location)
//            //    .Distinct().ToList();
//        }

//        private bool IsFloor(XYLocation<FacilitySpace> x)
//        {
//            return x.Obj.Contains(FacilityObjectNames.Floor);
//        }

//        private void PutWallSpace(FacilityLayer layer, XY location)
//        {
//            var wallType = GetWallType(GetNearbyFloorLocations(layer, location));
//            //layer[location].LowerObject = WallFactory.Create("Wall-" + wallType);
//        }

//        private List<int> GetNearbyFloorLocations(FacilityLayer layer, XY location)
//        {
//            return new List<int>();
//            //return layer.GetNeighbors(location)
//            //    .Where(IsFloor)
//            //    .Select(x => GetSpaceNumber(x.Location, location))
//            //    .OrderBy(x => x).ToList();
//        }

//        private string GetWallType(List<int> floorLocations)
//        {
//            var wallType = "";
//            floorLocations.ForEach(x => wallType += x);
//            return wallType;
//        }

//        private int GetSpaceNumber(XY spaceLocation, XY wallLocation)
//        {
//            var offset = wallLocation.GetOffset(spaceLocation);
//            return _offsetNeightborCoordinates[offset];
//        }
//    }
//}