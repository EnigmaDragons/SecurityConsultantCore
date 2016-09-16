using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurityConsultantCore.EngineInterfaces;
using SecurityConsultantCore.EventSystem;
using SecurityConsultantCore.EventSystem.Events;
using SecurityConsultantCore.Security.Alarms;
using SecurityConsultantCore.Test.EngineMocks;
using System.Diagnostics.CodeAnalysis;

namespace SecurityConsultantCore.Test.Security.Alarms
{
    [TestClass, ExcludeFromCodeCoverage]
    public class AdvancedAlarmTests
    {
        private IEventAggregator _eventAggregator = new EventAggregator();
        private SoundMock _sound = new SoundMock();

        [TestMethod]
        public void AdvancedAlarm_Triggered_SoundsAlarm()
        {
            var sut = new AdvancedAlarm(_eventAggregator, _sound);

            sut.Trigger();

            Assert.IsTrue(_sound.Played);
        }

        [TestMethod]
        public void AdvancedAlarm_Triggered_AlertsSecurity()
        {
            var sut = new AdvancedAlarm(_eventAggregator, _sound);
            var securityAlerted = false;
            _eventAggregator.Subscribe<AlertSecurityEvent>(e => securityAlerted = true);

            sut.Trigger();

            Assert.IsTrue(securityAlerted);
        }

        [TestMethod]
        public void AdvancedAlarm_TurnedOff_TurnsOffAlarm()
        {
            var sut = new AdvancedAlarm(_eventAggregator, _sound);
            sut.Trigger();

            sut.TurnOff();

            Assert.IsTrue(_sound.Stopped);
        }
    }

    public class AdvancedAlarm : AlarmBase, IAlarm
    {
        private IEventAggregator _eventAggregator;
        private ISound _alarmSound;

        public AdvancedAlarm(IEventAggregator eventAggregator, ISound alarmSound)
        {
            _eventAggregator = eventAggregator;
            _alarmSound = alarmSound;
        }

        public void Trigger()
        {
            _eventAggregator.Publish(new AlarmSoundEvent());
            _eventAggregator.Publish(new AlertSecurityEvent());
            _alarmSound.Play();
        }

        public void TurnOff()
        {
            _alarmSound.Stop();
        }
    }
}
