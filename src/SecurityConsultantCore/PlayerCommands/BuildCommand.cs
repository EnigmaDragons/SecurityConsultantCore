using System;
using SecurityConsultantCore.Domain;
using SecurityConsultantCore.Domain.Basic;

namespace SecurityConsultantCore.PlayerCommands
{
    public class BuildCommand
    {
        private FacilityMap _map;
        private SecurityObject _securityObject;
        private XYZ _location;

        public BuildCommand(FacilityMap map, SecurityObject securityObject, XYZ location)
        {
            _map = map;
            _securityObject = securityObject;
            _location = location;
        }

        public void Go()
        {
            _map[_location].Put(_securityObject);
        }
    }
}
