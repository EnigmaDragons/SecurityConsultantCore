using System.Linq;
using SecurityConsultantCore.Domain;
using SecurityConsultantCore.Domain.Basic;

namespace SecurityConsultantCore.PlayerCommands
{
    public class EditCommand
    {
        private readonly FacilityMap _map;
        private readonly XYZ _location;
        private readonly IEngineer _engineer;

        public EditCommand(FacilityMap map, XYZ location, IEngineer engineer)
        {
            _map = map;
            _location = location;
            _engineer = engineer;
        }

        public void Go()
        {
            if (!_map.Exists(_location) || !_map[_location].Placeables.Any())
                return;

            var securityObject = _map[_location].Placeables.First();
            securityObject.ConsultWith(_engineer);
        }
    }
}
