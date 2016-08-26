using System;
using SecurityConsultantCore.Domain;

namespace SecurityConsultantCore.GameState
{
    public static class CurrentMap
    {
        private static FacilityMap _map;

        public static FacilityMap Get()
        {
            if (_map == null)
                throw new InvalidOperationException("Current map not set.");
            return _map;
        }

        public static void Set(FacilityMap map)
        {
            _map = map;
        }
    }
}