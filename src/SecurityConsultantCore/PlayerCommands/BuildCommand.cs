﻿using System;
using SecurityConsultantCore.Domain;
using SecurityConsultantCore.Domain.Basic;

namespace SecurityConsultantCore.PlayerCommands
{
    public class BuildCommand
    {
        private FacilityMap _map;
        private SecurityObjectBase _securityObject;
        private XYZ _location;

        public BuildCommand(FacilityMap map, SecurityObjectBase securityObject, XYZ location)
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