namespace SecurityConsultantCore.Security.Alarms
{
    public abstract class AlarmBase
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

        public abstract void Trigger();
        public abstract void TurnOff();
    }
}
