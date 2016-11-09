using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurityConsultantCore.Domain.Basic;
using SecurityConsultantCore.EventSystem;
using SecurityConsultantCore.EventSystem.Events;
using SecurityConsultantCore.Security.Alarms;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace SecurityConsultantCore.Test.Security.Alarms
{
    [TestClass, ExcludeFromCodeCoverage]
    public class SilentAlarmTests
    {
        private IEventAggregator _eventAggregator = new EventAggregator();

        [TestMethod]
        public void SilentAlarm_Triggered_FiresAlertEvent()
        {
            var sut = new SilentAlarm(_eventAggregator);
            var alerted = false;
            _eventAggregator.Subscribe<AlertSecurityEvent>(e => alerted = true);

            sut.Trigger(new XY());

            Assert.IsTrue(alerted);
        }

        [TestMethod]
        public void SilentAlarm_Disarmed_TriggerDoesNothing()
        {
            var sut = new SilentAlarm(_eventAggregator);
            var alerted = false;
            _eventAggregator.Subscribe<AlertSecurityEvent>(e => alerted = true);

            sut.Disarm();
            sut.Trigger(new XY());

            Assert.IsFalse(alerted);
        }

        [TestMethod]
        public void SilentAlarm_TriggeredMultipleTimes_OnlyCallsSecurityOnce()
        {
            var sut = new SilentAlarm(_eventAggregator);
            var timesSecurityCalled = 0;
            _eventAggregator.Subscribe<AlertSecurityEvent>(e => timesSecurityCalled++);

            foreach (var _ in Enumerable.Range(0, 5))
                sut.Trigger(new XY());

            Assert.AreEqual(1, timesSecurityCalled);
        }

        [TestMethod]
        public void SilentAlarm_WhenTurnedOff_CanAlertSecurityAgain()
        {
            var sut = new SilentAlarm(_eventAggregator);
            var timesSecurityCalled = 0;
            _eventAggregator.Subscribe<AlertSecurityEvent>(e => timesSecurityCalled++);

            sut.Trigger(new XY());
            sut.TurnOff();
            sut.Trigger(new XY());

            Assert.AreEqual(2, timesSecurityCalled);
        }
    }
}
