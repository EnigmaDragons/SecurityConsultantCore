namespace SecurityConsultantCore.Security.Alarms
{
    public class AlarmBase
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
