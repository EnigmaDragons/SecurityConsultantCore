﻿using SecurityConsultantCore.EventSystem;
using SecurityConsultantCore.EventSystem.EventTypes;
﻿using SecurityConsultantCore.Domain.Basic;

namespace SecurityConsultantCore.Security.Alarms
{
    public class SilentAlarm : AlarmBase, IAlarm
    {
        private readonly IEvents _events;
        private bool _securityAlerted;

        public SilentAlarm(IEvents events)
        {
            _events = events;
        }

        public void Trigger(XY _)
        {
            if(IsArmed && !_securityAlerted)
            {
                _events.Publish(new AlertSecurityEvent());
                _securityAlerted = true;
            }
        }

        public void TurnOff()
        {
            _securityAlerted = false;
        }
    }
}
