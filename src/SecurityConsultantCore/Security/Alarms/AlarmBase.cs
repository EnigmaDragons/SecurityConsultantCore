﻿namespace SecurityConsultantCore.Security.Alarms
{
    public class AlarmBase : IArmable
    {
        public bool IsArmed { get; private set; }

        public void Arm()
        {
            IsArmed = true;
        }

        public void Disarm()
        {
            IsArmed = false;
        }
    }
}
