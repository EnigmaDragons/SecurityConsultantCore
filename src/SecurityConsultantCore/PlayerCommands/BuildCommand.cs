using SecurityConsultantCore.Domain;
using SecurityConsultantCore.Domain.Basic;

namespace SecurityConsultantCore.PlayerCommands
{
    public class BuildCommand
    {
        private readonly FacilityMap _map;
        private readonly SecurityObjectBase _securityObject;
        private readonly XYZ _location;

        public BuildCommand(FacilityMap map, SecurityObjectBase securityObject, XYZ location)
        {
            _map = map;
            _securityObject = securityObject;
            _location = location;
        }

        public void Go()
        {
            if (IsValid())
                _map[_location].Put(_securityObject);
        }

        private bool IsValid()
        {
            return _map.Exists(_location) 
                && _map[_location][_securityObject.ObjectLayer].IsNothing 
                && !_map[_location].LowerObject.Type.Equals("Wall");
        }
    }
}
