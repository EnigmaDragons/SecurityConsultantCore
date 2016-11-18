using System.Collections.Generic;
using System.Linq;
using SecurityConsultantCore.Domain;
using SecurityConsultantCore.Domain.Basic;

namespace SecurityConsultantCore.Factories
{
    public static class WallFactory
    {
        private static Dictionary<string, FacilityObject> _walls;

        private static Dictionary<string, FacilityObject> Walls
        {
            get
            {
                if (_walls == null)
                    Init();
                return _walls;
            }
        }

        public static FacilityObject Create(string wallType)
        {
            var key = CleanKey(wallType);
            if (string.IsNullOrEmpty(key) || !Walls.ContainsKey(key))
                throw new KeyNotFoundException("Unknown wall type: \"" + key + "\"");
            return Walls[key];
        }

        private static string CleanKey(string wallType)
        {
            var result = wallType;
            if (result.Contains("2") || result.Contains("4"))
                result = result.Replace("1", "");
            if (result.Contains("2") || result.Contains("6"))
                result = result.Replace("3", "");
            if (result.Contains("4") || result.Contains("8"))
                result = result.Replace("7", "");
            if (result.Contains("6") || result.Contains("8"))
                result = result.Replace("9", "");
            return result;
        }

        private static void Init()
        {
            _walls = new Dictionary<string, FacilityObject>
            {
                {"Column", Create(FacilityObjectNames.WallPillar, Orientation.None)},
                {"Wall-2", Create(FacilityObjectNames.WallSide, Orientation.Up)},
                {"Wall-4", Create(FacilityObjectNames.WallSide, Orientation.Left)},
                {"Wall-6", Create(FacilityObjectNames.WallSide, Orientation.Right)},
                {"Wall-8", Create(FacilityObjectNames.WallSide, Orientation.Down)},
                {"Wall-1", Create(FacilityObjectNames.WallCorner, Orientation.UpLeft)},
                {"Wall-3", Create(FacilityObjectNames.WallCorner, Orientation.UpRight)},
                {"Wall-7", Create(FacilityObjectNames.WallCorner, Orientation.DownLeft)},
                {"Wall-9", Create(FacilityObjectNames.WallCorner, Orientation.DownRight)},
                {"Wall-48", Create(FacilityObjectNames.WallOuterCorner, Orientation.DownLeft)},
                {"Wall-24", Create(FacilityObjectNames.WallOuterCorner, Orientation.UpLeft)},
                {"Wall-68", Create(FacilityObjectNames.WallOuterCorner, Orientation.DownRight)},
                {"Wall-26", Create(FacilityObjectNames.WallOuterCorner, Orientation.UpRight)},
                {"Wall-19", Create(FacilityObjectNames.WallDoubleCornerDiagonal, Orientation.DownRight)},
                {"Wall-37", Create(FacilityObjectNames.WallDoubleCornerDiagonal, Orientation.UpRight)},
                {"Wall-1379", Create(FacilityObjectNames.WallQuadraCorner, Orientation.None)},
                {"Wall-28", Create(FacilityObjectNames.WallDoubleSide, Orientation.Up)},
                {"Wall-46", Create(FacilityObjectNames.WallDoubleSide, Orientation.Right)},
                {"Wall-2468", Create(FacilityObjectNames.WallPillar, Orientation.None)},
                {"Wall-29", Create(FacilityObjectNames.WallSideCornerRight, Orientation.Up)},
                {"Wall-34", Create(FacilityObjectNames.WallSideCornerRight, Orientation.Left)},
                {"Wall-67", Create(FacilityObjectNames.WallSideCornerRight, Orientation.Right)},
                {"Wall-18", Create(FacilityObjectNames.WallSideCornerRight, Orientation.Down)},
                {"Wall-27", Create(FacilityObjectNames.WallSideCornerLeft, Orientation.Up)},
                {"Wall-49", Create(FacilityObjectNames.WallSideCornerLeft, Orientation.Left)},
                {"Wall-16", Create(FacilityObjectNames.WallSideCornerLeft, Orientation.Right)},
                {"Wall-38", Create(FacilityObjectNames.WallSideCornerLeft, Orientation.Down)},
                {"Wall-249", Create(FacilityObjectNames.WallOuterDoubleCorner, Orientation.UpLeft)},
                {"Wall-267", Create(FacilityObjectNames.WallOuterDoubleCorner, Orientation.UpRight)},
                {"Wall-348", Create(FacilityObjectNames.WallOuterDoubleCorner, Orientation.DownLeft)},
                {"Wall-168", Create(FacilityObjectNames.WallOuterDoubleCorner, Orientation.DownRight)},
                {"Wall-13", Create(FacilityObjectNames.WallDoubleCorner, Orientation.Up)},
                {"Wall-17", Create(FacilityObjectNames.WallDoubleCorner, Orientation.Left)},
                {"Wall-39", Create(FacilityObjectNames.WallDoubleCorner, Orientation.Right)},
                {"Wall-79", Create(FacilityObjectNames.WallDoubleCorner, Orientation.Down)},
                {"Wall-246", Create(FacilityObjectNames.WallEnd, Orientation.Up)},
                {"Wall-248", Create(FacilityObjectNames.WallEnd, Orientation.Left)},
                {"Wall-268", Create(FacilityObjectNames.WallEnd, Orientation.Right)},
                {"Wall-468", Create(FacilityObjectNames.WallEnd, Orientation.Down)},
                {"Wall-139", Create(FacilityObjectNames.WallTripleCorner, Orientation.UpRight)},
                {"Wall-137", Create(FacilityObjectNames.WallTripleCorner, Orientation.UpLeft)},
                {"Wall-379", Create(FacilityObjectNames.WallTripleCorner, Orientation.DownRight)},
                {"Wall-179", Create(FacilityObjectNames.WallTripleCorner, Orientation.DownLeft)},
                {"Wall-138", Create(FacilityObjectNames.WallSideDoubleCorner, Orientation.Down)},
                {"Wall-167", Create(FacilityObjectNames.WallSideDoubleCorner, Orientation.Right)},
                {"Wall-349", Create(FacilityObjectNames.WallSideDoubleCorner, Orientation.Left)},
                {"Wall-279", Create(FacilityObjectNames.WallSideDoubleCorner, Orientation.Up)}
            };
        }

        private static FacilityObject Create(string type, Orientation orientation)
        {
            return new StructureObject {Type = type, Orientation = orientation };
        }

        public static List<string> GetConstructables()
        {
            return Walls.Select(x => x.Key).ToList();
        }
    }
}