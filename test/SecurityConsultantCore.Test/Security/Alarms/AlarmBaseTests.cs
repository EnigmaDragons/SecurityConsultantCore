using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using SecurityConsultantCore.Security.Alarms;

namespace SecurityConsultantCore.Test.Security.Alarms
{
    internal class AlarmBaseMock : AlarmBase
    {
        public override void Trigger()
        {
            throw new NotImplementedException();
        }

        public override void TurnOff()
        {
            throw new NotImplementedException();
        }
    }

    [TestClass, ExcludeFromCodeCoverage]
    public class AlarmBaseTests
    {
        [TestMethod]
        public void AlarmBase_Arm_IsArmedTrue()
        {
            var alarm = new AlarmBaseMock();

            alarm.Arm();

            Assert.IsTrue(alarm.IsArmed);
        }

        [TestMethod]
        public void AlarmBase_Disarmed_IsArmedFalse()
        {
            var alarm = new AlarmBaseMock();

            alarm.Disarm();

            Assert.IsFalse(alarm.IsArmed);
        }
    }
}
