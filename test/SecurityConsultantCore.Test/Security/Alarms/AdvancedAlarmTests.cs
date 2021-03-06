﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurityConsultantCore.Domain.Basic;
using SecurityConsultantCore.Security.Alarms;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using SecurityConsultantCore.Engine;
using SecurityConsultantCore.Event;
using SecurityConsultantCore.Test.Engine;

namespace SecurityConsultantCore.Test.Security.Alarms
{
    [TestClass, ExcludeFromCodeCoverage]
    public class AdvancedAlarmTests
    {
        private readonly IEvents _events = new Events();
        private readonly SoundMock _sound = new SoundMock();

        [TestMethod]
        public void AdvancedAlarm_Triggered_SoundsAlarm()
        {
            var sut = new AdvancedAlarm(_events, _sound);

            sut.Trigger(new XY());

            Assert.IsTrue(_sound.Played);
        }

        [TestMethod]
        public void AdvancedAlarm_Triggered_AlertsSecurity()
        {
            var sut = new AdvancedAlarm(_events, _sound);
            var securityAlerted = false;
            _events.Subscribe<PositionedAlertSecurityEvent>(e => securityAlerted = true);

            sut.Trigger(new XY());

            Assert.IsTrue(securityAlerted);
        }

        [TestMethod]
        public void AdvancedAlarm_TurnedOff_TurnsOffAlarm()
        {
            var sut = new AdvancedAlarm(_events, _sound);
            sut.Trigger(new XY());

            sut.TurnOff();

            Assert.IsTrue(_sound.Stopped);
        }

        [TestMethod]
        public void AdvancedAlarm_Disarmed_TriggerDoesNothing()
        {
            var sut = new AdvancedAlarm(_events, _sound);
            var securityAlerted = false;
            _events.Subscribe<PositionedAlertSecurityEvent>(e => securityAlerted = true);
            sut.Disarm();

            sut.Trigger(new XY());

            Assert.IsFalse(securityAlerted);
        }

        [TestMethod]
        public void AdvancedAlarm_Triggered_AlertSecurityEventHoldsPassedInTriggerPosition()
        {
            var sut = new AdvancedAlarm(_events, _sound);
            var expectedPosition = new XY(123, 123);
            var actualPosition = new XY(0, 0);
            _events.Subscribe<PositionedAlertSecurityEvent>(e => actualPosition = e.TriggerLocation);

            sut.Trigger(expectedPosition);

            Assert.AreEqual(expectedPosition, actualPosition);
        }

        [TestMethod]
        public void AdvancedAlarm_TriggeredMultipleTimes_OnlyCallsSecurityOnce()
        {
            var sut = new AdvancedAlarm(_events, _sound);
            var timesSecurityCalled = 0;
            _events.Subscribe<PositionedAlertSecurityEvent>(e => timesSecurityCalled++);

            foreach(var _ in Enumerable.Range(0, 5))
                sut.Trigger(new XY());

            Assert.AreEqual(1, timesSecurityCalled);
        }

        [TestMethod]
        public void AdvancedAlarm_WhenTurnedOff_CanAlertSecurityAgain()
        {
            var sut = new AdvancedAlarm(_events, _sound);
            var timesSecurityCalled = 0;
            _events.Subscribe<PositionedAlertSecurityEvent>(e => timesSecurityCalled++);
            sut.Trigger(new XY());

            sut.TurnOff();
            sut.Trigger(new XY());

            Assert.AreEqual(2, timesSecurityCalled);
        }
    }
}
