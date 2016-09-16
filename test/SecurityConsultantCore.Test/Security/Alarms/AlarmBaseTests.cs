using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using SecurityConsultantCore.Security.Alarms;

namespace SecurityConsultantCore.Test.Security.Alarms
{
    [TestClass, ExcludeFromCodeCoverage]
    public class AlarmBaseTests
    {
        [TestMethod]
        public void AlarmBase_Arm_IsArmedTrue()
        {
            var alarm = new AlarmBase();

            alarm.Arm();

            Assert.IsTrue(alarm.IsArmed);
        }

        [TestMethod]
        public void AlarmBase_Disarmed_IsArmedFalse()
        {
            var alarm = new AlarmBase();

            alarm.Disarm();

            Assert.IsFalse(alarm.IsArmed);
        }
    }
}
