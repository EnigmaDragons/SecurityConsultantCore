﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurityConsultantCore.Domain.Basic;
using SecurityConsultantCore.Security.Alarms;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using SecurityConsultantCore.Engine;
using SecurityConsultantCore.Event;

namespace SecurityConsultantCore.Test.Security.Alarms
{
    [TestClass, ExcludeFromCodeCoverage]
    public class SilentAlarmTests
    {
        private readonly IEvents _events = new Events();

        [TestMethod]
        public void SilentAlarm_Triggered_FiresAlertEvent()
        {
            var sut = new SilentAlarm(_events);
            var alerted = false;
            _events.Subscribe<AlertSecurityEvent>(e => alerted = true);

            sut.Trigger(new XY());

            Assert.IsTrue(alerted);
        }

        [TestMethod]
        public void SilentAlarm_Disarmed_TriggerDoesNothing()
        {
            var sut = new SilentAlarm(_events);
            var alerted = false;
            _events.Subscribe<AlertSecurityEvent>(e => alerted = true);

            sut.Disarm();
            sut.Trigger(new XY());

            Assert.IsFalse(alerted);
        }

        [TestMethod]
        public void SilentAlarm_TriggeredMultipleTimes_OnlyCallsSecurityOnce()
        {
            var sut = new SilentAlarm(_events);
            var timesSecurityCalled = 0;
            _events.Subscribe<AlertSecurityEvent>(e => timesSecurityCalled++);

            foreach (var _ in Enumerable.Range(0, 5))
                sut.Trigger(new XY());

            Assert.AreEqual(1, timesSecurityCalled);
        }

        [TestMethod]
        public void SilentAlarm_WhenTurnedOff_CanAlertSecurityAgain()
        {
            var sut = new SilentAlarm(_events);
            var timesSecurityCalled = 0;
            _events.Subscribe<AlertSecurityEvent>(e => timesSecurityCalled++);

            sut.Trigger(new XY());
            sut.TurnOff();
            sut.Trigger(new XY());

            Assert.AreEqual(2, timesSecurityCalled);
        }
    }
}
