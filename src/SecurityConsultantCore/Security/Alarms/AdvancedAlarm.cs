using SecurityConsultantCore.EngineInterfaces;
using SecurityConsultantCore.EventSystem;
using SecurityConsultantCore.EventSystem.Events;

namespace SecurityConsultantCore.Security.Alarms
{
    public class AdvancedAlarm : AlarmBase, IAlarm
    {
        private IEventAggregator _eventAggregator;
        private ISound _alarmSound;

        public AdvancedAlarm(IEventAggregator eventAggregator, ISound alarmSound)
        {
            _eventAggregator = eventAggregator;
            _alarmSound = alarmSound;
        }

        public void Trigger()
        {
            _eventAggregator.Publish(new AlertSecurityEvent());
            _alarmSound.Play();
        }

        public void TurnOff()
        {
            _alarmSound.Stop();
        }
    }
}
