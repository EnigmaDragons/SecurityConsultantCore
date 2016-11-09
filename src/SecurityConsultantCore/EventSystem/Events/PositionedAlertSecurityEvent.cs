using SecurityConsultantCore.Domain.Basic;

namespace SecurityConsultantCore.EventSystem.Events
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
