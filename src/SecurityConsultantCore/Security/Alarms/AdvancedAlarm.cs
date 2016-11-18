using SecurityConsultantCore.Domain.Basic;
using SecurityConsultantCore.EngineInterfaces;
using SecurityConsultantCore.EventSystem;
using SecurityConsultantCore.EventSystem.EventTypes;

namespace SecurityConsultantCore.Security.Alarms
{
    public class AdvancedAlarm : AlarmBase, IAlarm
    {
        private readonly IEvents _events;
        private readonly ISound _alarmSound;
        private bool _securityAlerted;

        public AdvancedAlarm(IEvents events, ISound alarmSound)
        {
            _events = events;
            _alarmSound = alarmSound;
        }

        public void Trigger(XY triggerLocation)
        {
            if(IsArmed && !_securityAlerted)
            {
                _events.Publish(new PositionedAlertSecurityEvent(triggerLocation));
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
