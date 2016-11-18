using SecurityConsultantCore.Domain;
using SecurityConsultantCore.Domain.Basic;

namespace SecurityConsultantCore.PlayerCommands
{
    public class EditCommand
    {
        private readonly FacilityMap _facilityMap;
        private readonly XYZ _xyz;
        private readonly IPlayerEdit _playerEdit;

        public EditCommand(FacilityMap facilityMap, XYZ xyz, IPlayerEdit playerEdit)
        {
            _facilityMap = facilityMap;
            _xyz = xyz;
            _playerEdit = playerEdit;
        }

        public void Go()
        {
            
        }
    }
}
