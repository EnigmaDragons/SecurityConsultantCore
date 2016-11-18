﻿using System.Linq;
using SecurityConsultantCore.Domain;
using SecurityConsultantCore.Domain.Basic;

namespace SecurityConsultantCore.PlayerCommands
{
    public class EraseCommand
    {
        private FacilityMap _map;
        private XYZ _location;

        public EraseCommand(FacilityMap map, XYZ location)
        {
            _map = map;
            _location = location;
        }

        public void Go()
        {
            _map[_location].Placeables.ToList().ForEach(x => _map[_location].Remove(x));
        }
    }
}
