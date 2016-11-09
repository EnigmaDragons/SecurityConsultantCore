using SecurityConsultantCore.Domain.Basic;
using SecurityConsultantCore.EngineInterfaces;
using SecurityConsultantCore.EventSystem;
using SecurityConsultantCore.EventSystem.EventTypes;

namespace SecurityConsultantCore.Security.Alarms
{
    public class AdvancedAlarm : AlarmBase, IAlarm
    {
        private readonly IEvents _eventNotification;
        private readonly ISound _alarmSound;
        private bool _securityAlerted;

        public AdvancedAlarm(IEvents eventNotification, ISound alarmSound)
        {
            _eventNotification = eventNotification;
            _alarmSound = alarmSound;
        }

        public void Trigger(XY triggerLocation)
        {
            if(IsArmed && !_securityAlerted)
            {
                _eventNotification.Publish(new PositionedAlertSecurityEvent(triggerLocation));
                _alarmSound.Play();
                _securityAlerted = true;
            }
        }

        public void TurnOff()
        {
            _alarmSound.Stop();
            _securityAlerted = false;
        }
    }
}
