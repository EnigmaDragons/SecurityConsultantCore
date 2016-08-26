using System;

namespace SecurityConsultantCore.Common
{
    public static class GameRandom
    {
        private static Random _rng;

        public static int Random(int maxValue)
        {
            return GetRandom().Next(maxValue);
        }

        public static int Random(int minValue, int maxValue)
        {
            return GetRandom().Next(minValue, maxValue);
        }

        private static Random GetRandom()
        {
            return _rng ?? (_rng = new Random(Guid.NewGuid().GetHashCode()));
        }
    }
}