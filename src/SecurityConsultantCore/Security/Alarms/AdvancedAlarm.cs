using SecurityConsultantCore.Domain.Basic;
using SecurityConsultantCore.EngineInterfaces;
using SecurityConsultantCore.EventSystem;
using SecurityConsultantCore.EventSystem.Events;

namespace SecurityConsultantCore.Security.Alarms
{
    public class AdvancedAlarm : AlarmBase, IAlarm
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ISound _alarmSound;
        private bool _securityAlerted;

        public AdvancedAlarm(IEventAggregator eventAggregator, ISound alarmSound)
        {
            _eventAggregator = eventAggregator;
            _alarmSound = alarmSound;
        }

        public void Trigger(XY triggerLocation)
        {
            if(IsArmed && !_securityAlerted)
            {
                _eventAggregator.Publish(new PositionedAlertSecurityEvent(triggerLocation));
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
