using SecurityConsultantCore.EngineInterfaces;

namespace SecurityConsultantCore.Security.Alarms
{
    public class BasicAlarm : AlarmBase, IAlarm
    {
        private ISound _alarmSound;

        public BasicAlarm(ISound alarmSound)
        {
            _alarmSound = alarmSound;
        }

        public void Trigger()
        {
            if (IsArmed)
                _alarmSound.Play();
        }

        public void TurnOff()
        {
            _alarmSound.Stop();
        }
    }
}
