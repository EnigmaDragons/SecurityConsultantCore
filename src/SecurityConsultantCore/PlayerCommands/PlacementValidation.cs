using System;
using SecurityConsultantCore.Domain;
using SecurityConsultantCore.Domain.Basic;

namespace SecurityConsultantCore.PlayerCommands
{
    public class PlacementValidation
    {
        private FacilityMap _map;
        private XYZ _location;
        private SecurityObject _securityObject;

        public PlacementValidation(FacilityMap map, SecurityObject securityObject, XYZ location)
        {
            _map = map;
            _securityObject = securityObject;
            _location = location;
        }

        public bool Check()
        {
            if (!_map.Exists(_location))
                return false;
            return _map[_location][_securityObject.ObjectLayer].Type == "None";
        }
    }
}
