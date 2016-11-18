using SecurityConsultantCore.Domain.Basic;

namespace SecurityConsultantCore.Security.Alarms
{
    public interface IAlarm : IArmable
    {
        void Trigger(XY triggerPosition);
        void TurnOff();
    }
}
