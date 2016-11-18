using SecurityConsultantCore.Domain;
using SecurityConsultantCore.Domain.Basic;
using System.Collections.Generic;
using System.Linq;

namespace SecurityConsultantCore.PlayerCommands
{
    public class InspectCommand
    {
        private readonly FacilityMap _map;
        private readonly XYZ _location;
        private readonly IPlayerNotification _playerNotification;

        public InspectCommand(FacilityMap map, XYZ location, IPlayerNotification playerNotification)
        {
            _map = map;
            _location = location;
            _playerNotification = playerNotification;
        }

        public void Go()
        {
            _map[_location].FacilityValuables.ToList().ForEach(x => _playerNotification.Notify(x));
            _map[_location].FacilityContainers.ToList().ForEach(x => _playerNotification.Notify(x));
        }
    }
}
