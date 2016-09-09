using SecurityConsultantCore.EngineInterfaces;

namespace SecurityConsultantCore.Security.Alarms
{
    public class BasicAlarm : AlarmBase
    {
        private ISound _alarmSound;

        public BasicAlarm(ISound alarmSound)
        {
            _alarmSound = alarmSound;
        }

        public override void Trigger()
        {
            if (IsArmed)
                _alarmSound.Play();
        }

        public override void TurnOff()
        {
            _alarmSound.Stop();
        }
    }
}
