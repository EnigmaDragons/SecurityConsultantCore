using SecurityConsultantCore.EngineInterfaces;
using SecurityConsultantCore.EventSystem;
using SecurityConsultantCore.EventSystem.EventTypes;

namespace SecurityConsultantCore.Security.Alarms
{
    public class AdvancedAlarm : AlarmBase, IAlarm
    {
        private IEvents _eventNotification;
        private ISound _alarmSound;

        public AdvancedAlarm(IEvents eventNotification, ISound alarmSound)
        {
            _eventNotification = eventNotification;
            _alarmSound = alarmSound;
        }

        public void Trigger()
        {
            if(IsArmed)
            {
                _eventNotification.Publish(new AlertSecurityEvent());
                _alarmSound.Play();
            }
        }

        public void TurnOff()
        {
            _alarmSound.Stop();
        }
    }
}
