﻿using SecurityConsultantCore.Domain.Basic;
﻿using SecurityConsultantCore.Engine;
﻿using SecurityConsultantCore.Event;

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
