using SecurityConsultantCore.Domain.Basic;

namespace SecurityConsultantCore.EventSystem.EventTypes
{
    public class PositionedAlertSecurityEvent
    {
        public PositionedAlertSecurityEvent(XY triggerLocation)
        {
            TriggerLocation = triggerLocation;
        }

        public XY TriggerLocation { get; }
    }
}
