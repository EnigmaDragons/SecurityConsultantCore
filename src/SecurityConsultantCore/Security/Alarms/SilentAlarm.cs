﻿using SecurityConsultantCore.EventSystem;
using SecurityConsultantCore.EventSystem.EventTypes;
﻿using SecurityConsultantCore.Domain.Basic;

namespace SecurityConsultantCore.Security.Alarms
{
    public class SilentAlarm : AlarmBase, IAlarm
    {
        private readonly IEvents _eventNotification;
        private bool _securityAlerted;

        public SilentAlarm(IEvents eventNotification)
        {
            _eventNotification = eventNotification;
        }

        public void Trigger(XY _)
        {
            if(IsArmed && !_securityAlerted)
            {
                _eventNotification.Publish(new AlertSecurityEvent());
                _securityAlerted = true;
            }
        }

        public void TurnOff()
        {
            _securityAlerted = false;
        }
    }
}
