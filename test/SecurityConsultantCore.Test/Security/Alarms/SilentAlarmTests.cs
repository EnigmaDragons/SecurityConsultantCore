using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurityConsultantCore.EventSystem;
using SecurityConsultantCore.Security.Alarms;
using System.Diagnostics.CodeAnalysis;
using SecurityConsultantCore.EventSystem.EventTypes;

namespace SecurityConsultantCore.Test.Security.Alarms
{
    [TestClass, ExcludeFromCodeCoverage]
    public class SilentAlarmTests
    {
        private IEvents _eventNotification = new Events();

        [TestMethod]
        public void SilentAlarm_Triggered_FiresAlertEvent()
        {
            var sut = new SilentAlarm(_eventNotification);
            var alerted = false;
            _eventNotification.Subscribe<AlertSecurityEvent>(e => alerted = true);

            sut.Trigger();

            Assert.IsTrue(alerted);
        }

        [TestMethod]
        public void SilentAlarm_Disarmed_TriggerDoesNothing()
        {
            var sut = new SilentAlarm(_eventNotification);
            var alerted = false;
            _eventNotification.Subscribe<AlertSecurityEvent>(e => alerted = true);

            sut.Disarm();
            sut.Trigger();

            Assert.IsFalse(alerted);
        }
    }
}
