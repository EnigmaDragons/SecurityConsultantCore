using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurityConsultantCore.Domain.Basic;
using SecurityConsultantCore.EventSystem;
using SecurityConsultantCore.Security.Alarms;
using SecurityConsultantCore.Test.EngineMocks;
using System.Diagnostics.CodeAnalysis;
using SecurityConsultantCore.EventSystem.EventTypes;
using System.Linq;

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

            sut.Trigger(new XY());

            Assert.IsTrue(_sound.Played);
        }

        [TestMethod]
        public void AdvancedAlarm_Triggered_AlertsSecurity()
        {
            var sut = new AdvancedAlarm(_eventNotification, _sound);
            var securityAlerted = false;
            _eventNotification.Subscribe<PositionedAlertSecurityEvent>(e => securityAlerted = true);

            sut.Trigger(new XY());

            Assert.IsTrue(securityAlerted);
        }

        [TestMethod]
        public void AdvancedAlarm_TurnedOff_TurnsOffAlarm()
        {
            var sut = new AdvancedAlarm(_eventNotification, _sound);
            sut.Trigger(new XY());

            sut.TurnOff();

            Assert.IsTrue(_sound.Stopped);
        }

        [TestMethod]
        public void AdvancedAlarm_Disarmed_TriggerDoesNothing()
        {
            var sut = new AdvancedAlarm(_eventNotification, _sound);
            var securityAlerted = false;
            _eventNotification.Subscribe<AlertSecurityEvent>(e => securityAlerted = true);
            _eventNotification.Subscribe<PositionedAlertSecurityEvent>(e => securityAlerted = true);
            sut.Disarm();

            sut.Trigger(new XY());

            Assert.IsFalse(securityAlerted);
        }

        [TestMethod]
        public void AdvancedAlarm_Triggered_AlertSecurityEventHoldsPassedInTriggerPosition()
        {
            var sut = new AdvancedAlarm(_eventNotification, _sound);
            var expectedPosition = new XY(123, 123);
            var actualPosition = new XY(0, 0);
            _eventNotification.Subscribe<PositionedAlertSecurityEvent>(e => actualPosition = e.TriggerLocation);

            sut.Trigger(expectedPosition);

            Assert.AreEqual(expectedPosition, actualPosition);
        }

        [TestMethod]
        public void AdvancedAlarm_TriggeredMultipleTimes_OnlyCallsSecurityOnce()
        {
            var sut = new AdvancedAlarm(_eventNotification, _sound);
            var timesSecurityCalled = 0;
            _eventNotification.Subscribe<PositionedAlertSecurityEvent>(e => timesSecurityCalled++);

            foreach(var _ in Enumerable.Range(0, 5))
                sut.Trigger(new XY());

            Assert.AreEqual(1, timesSecurityCalled);
        }

        [TestMethod]
        public void AdvancedAlarm_WhenTurnedOff_CanAlertSecurityAgain()
        {
            var sut = new AdvancedAlarm(_eventNotification, _sound);
            var timesSecurityCalled = 0;
            _eventNotification.Subscribe<PositionedAlertSecurityEvent>(e => timesSecurityCalled++);
            sut.Trigger(new XY());

            sut.TurnOff();
            sut.Trigger(new XY());

            Assert.AreEqual(2, timesSecurityCalled);
        }
    }
}
