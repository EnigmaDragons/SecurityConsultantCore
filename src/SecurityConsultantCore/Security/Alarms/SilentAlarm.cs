using SecurityConsultantCore.Domain.Basic;
using SecurityConsultantCore.EventSystem;
using SecurityConsultantCore.EventSystem.Events;

namespace SecurityConsultantCore.Security.Alarms
{
    public class SilentAlarm : AlarmBase, IAlarm
    {
        private readonly IEventAggregator _eventAggregator;
        private bool _securityAlerted;

        public SilentAlarm(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void Trigger(XY _)
        {
            if(IsArmed && !_securityAlerted)
            {
                _eventAggregator.Publish(new AlertSecurityEvent());
                _securityAlerted = true;
            }
        }

        public void TurnOff()
        {
            _securityAlerted = false;
        }
    }
}
