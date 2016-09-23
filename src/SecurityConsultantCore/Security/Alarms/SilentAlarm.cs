using SecurityConsultantCore.EventSystem;
using SecurityConsultantCore.EventSystem.Events;

namespace SecurityConsultantCore.Security.Alarms
{
    public class SilentAlarm : AlarmBase, IAlarm
    {
        IEventAggregator _eventAggregator;

        public SilentAlarm(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void Trigger()
        {
            _eventAggregator.Publish(new AlertSecurityEvent());
        }

        public void TurnOff()
        {
        }
    }
}
