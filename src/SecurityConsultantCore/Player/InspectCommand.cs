using System.Linq;
using SecurityConsultantCore.Domain;
using SecurityConsultantCore.Domain.Basic;

namespace SecurityConsultantCore.Player
{
    public class InspectCommand
    {
        private readonly FacilityMap _map;
        private readonly XYZ _location;
        private readonly IInspector _inspector;

        public InspectCommand(FacilityMap map, XYZ location, IInspector inspector)
        {
            _map = map;
            _location = location;
            _inspector = inspector;
        }

        public void Go()
        {
            if (!_map.Exists(_location))
                return;

            _map[_location].FacilityValuables.ToList().ForEach(x => _inspector.Notify(x));
            _map[_location].FacilityContainers.ToList().ForEach(x => _inspector.Notify(x));
        }
    }
}
