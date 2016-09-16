using System.Collections.Generic;

namespace SecurityConsultantCore.Domain.Basic
{
    public class Orientation
    {
        private static Dictionary<string, Orientation> _abbreviations;

        private Orientation(string desc, int rotation)
        {
            Description = desc;
            Rotation = rotation;
        }

        public string Description { get; }
        public int Rotation { get; }

        public static Orientation None { get; } = new Orientation("None", 0);
        public static Orientation Up { get; } = new Orientation("Up", 0);
        public static Orientation UpRight { get; } = new Orientation("UpRight", 0);
        public static Orientation Right { get; } = new Orientation("Right", 270);
        public static Orientation DownRight { get; } = new Orientation("DownRight", 270);
        public static Orientation Down { get; } = new Orientation("Down", 180);
        public static Orientation DownLeft { get; } = new Orientation("DownLeft", 180);
        public static Orientation Left { get; } = new Orientation("Left", 90);
        public static Orientation UpLeft { get; } = new Orientation("UpLeft", 90);

        public static IEnumerable<Orientation> AllOrientations { get; } = new List<Orientation> { Up, Right, Down, Left };
    
        private static Dictionary<string, Orientation> Abbreviations
        {
            get
            {
                if (_abbreviations == null)
                    Init();
                return _abbreviations;
            }
        }

        public new string ToString()
        {
            return Description;
        }

        private static void Init()
        {
            _abbreviations = new Dictionary<string, Orientation>
            {
                {"u", Up},
                {"d", Down},
                {"l", Left},
                {"r", Right},
                {"ul", UpLeft},
                {"ur", UpRight},
                {"dl", DownLeft},
                {"dr", DownRight}
            };
        }

        public static Orientation FromAbbreviation(string s)
        {
            var key = s.ToLowerInvariant();
            try
            {
                return Abbreviations[key];
            }
            catch (KeyNotFoundException)
            {
                throw new KeyNotFoundException(key);
            }
        }
    }
}