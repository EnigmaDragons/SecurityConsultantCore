namespace SecurityConsultantCore.Security.Alarms
{
    public interface IAlarm
    {
        bool IsArmed { get; }
        void Arm();
        void Disarm();
        void Trigger();
        void TurnOff();
    }
}
