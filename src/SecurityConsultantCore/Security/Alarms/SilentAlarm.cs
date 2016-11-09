using SecurityConsultantCore.EventSystem;
using SecurityConsultantCore.EventSystem.EventTypes;

namespace SecurityConsultantCore.Security.Alarms
{
    public class SilentAlarm : AlarmBase, IAlarm
    {
        IEvents _eventNotification;

        public SilentAlarm(IEvents eventNotification)
        {
            _eventNotification = eventNotification;
        }

        public void Trigger()
        {
            if(IsArmed)
                _eventNotification.Publish(new AlertSecurityEvent());
        }

        public void TurnOff()
        {
        }
    }
}
