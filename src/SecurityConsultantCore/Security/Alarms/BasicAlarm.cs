using SecurityConsultantCore.Domain.Basic;
using SecurityConsultantCore.Engine;

namespace SecurityConsultantCore.Security.Alarms
{
    public class BasicAlarm : AlarmBase, IAlarm
    {
        private readonly ISound _alarmSound;

        public BasicAlarm(ISound alarmSound)
        {
            _alarmSound = alarmSound;
        }

        public void Trigger(XY _)
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
