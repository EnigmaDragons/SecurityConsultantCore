using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurityConsultantCore.EventSystem;
using SecurityConsultantCore.Security.Alarms;
using SecurityConsultantCore.Test.EngineMocks;
using System.Diagnostics.CodeAnalysis;
using SecurityConsultantCore.EventSystem.EventTypes;

namespace SecurityConsultantCore.Test.Security.Alarms
{
    [TestClass, ExcludeFromCodeCoverage]
    public class AdvancedAlarmTests
    {
        private IEvents _eventNotification = new Events();
        private SoundMock _sound = new SoundMock();

        [TestMethod]
        public void AdvancedAlarm_Triggered_SoundsAlarm()
        {
            var sut = new AdvancedAlarm(_eventNotification, _sound);

            sut.Trigger();

            Assert.IsTrue(_sound.Played);
        }

        [TestMethod]
        public void AdvancedAlarm_Triggered_AlertsSecurity()
        {
            var sut = new AdvancedAlarm(_eventNotification, _sound);
            var securityAlerted = false;
            _eventNotification.Subscribe<AlertSecurityEvent>(e => securityAlerted = true);

            sut.Trigger();

            Assert.IsTrue(securityAlerted);
        }

        [TestMethod]
        public void AdvancedAlarm_TurnedOff_TurnsOffAlarm()
        {
            var sut = new AdvancedAlarm(_eventNotification, _sound);
            sut.Trigger();

            sut.TurnOff();

            Assert.IsTrue(_sound.Stopped);
        }

        [TestMethod]
        public void AdvancedAlarm_Disarmed_TriggerDoesNothing()
        {
            var sut = new AdvancedAlarm(_eventNotification, _sound);
            var securityAlerted = false;
            _eventNotification.Subscribe<AlertSecurityEvent>(e => securityAlerted = true);
            sut.Disarm();

            sut.Trigger();

            Assert.IsFalse(securityAlerted);
        }
    }
}
