using SecurityConsultantCore.Domain.Basic;

namespace SecurityConsultantCore.Event
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
