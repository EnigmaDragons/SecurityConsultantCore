namespace SecurityConsultantCore.Security
{
    public interface IArmable
    {
        bool IsArmed { get; }
        void Arm();
        void Disarm();
    }
}
