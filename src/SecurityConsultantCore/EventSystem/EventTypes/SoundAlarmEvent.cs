namespace SecurityConsultantCore.EventSystem.EventTypes
{
    public class AlarmSoundEvent
    {
        public AlarmSoundEvent(bool turnSoundOn)
        {
            TurnSoundOn = turnSoundOn;
        }

        public bool TurnSoundOn { get; }
    }
}
