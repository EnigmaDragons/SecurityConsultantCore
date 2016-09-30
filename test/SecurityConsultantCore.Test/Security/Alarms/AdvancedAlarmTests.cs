using Microsoft.VisualStudio.TestTools.UnitTesting;
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

        [TestMethod]
        public void AdvancedAlarm_Disarmed_TriggerDoesNothing()
        {
            var sut = new AdvancedAlarm(_eventAggregator, _sound);
            var securityAlerted = false;
            _eventAggregator.Subscribe<AlertSecurityEvent>(e => securityAlerted = true);
            sut.Disarm();

            sut.Trigger();

            Assert.IsFalse(securityAlerted);
        }
    }
}
