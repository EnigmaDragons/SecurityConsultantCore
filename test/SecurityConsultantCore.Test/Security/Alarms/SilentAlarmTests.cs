using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurityConsultantCore.EventSystem;
using SecurityConsultantCore.EventSystem.Events;
using SecurityConsultantCore.Security.Alarms;
using System.Diagnostics.CodeAnalysis;

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

            sut.Trigger();

            Assert.IsTrue(alerted);
        }
    }
}
