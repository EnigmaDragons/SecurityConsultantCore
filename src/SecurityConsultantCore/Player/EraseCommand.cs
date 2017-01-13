using System.Linq;
using SecurityConsultantCore.Domain;
using SecurityConsultantCore.Domain.Basic;

namespace SecurityConsultantCore.Player
{
    public class EraseCommand
    {
        private readonly FacilityMap _map;
        private readonly XYZ _location;

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
