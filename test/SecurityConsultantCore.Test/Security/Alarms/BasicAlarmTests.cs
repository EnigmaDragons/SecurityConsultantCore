using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurityConsultantCore.Domain.Basic;
using SecurityConsultantCore.Security.Alarms;
using System.Diagnostics.CodeAnalysis;
using SecurityConsultantCore.Test.Engine;

namespace SecurityConsultantCore.Test.Security.Alarms
{
    [TestClass, ExcludeFromCodeCoverage]
    public class BasicAlarmTests
    {
        private SoundMock _sound = new SoundMock();
        private BasicAlarm _sut;

        [TestInitialize]
        public void Initialize()
        {
            _sut = new BasicAlarm(_sound);
        }

        [TestMethod]
        public void BasicAlarm_ArmedAndTriggered_PlaysFacilitySound()
        {
            _sut.Arm();

            _sut.Trigger(new XY());

            Assert.IsTrue(_sound.Played);
        }

        [TestMethod]
        public void BasicAlarm_TriggeredWhileDisarmed_PlaysNoSound()
        {
            _sut.Disarm();

            _sut.Trigger(new XY());

            Assert.IsFalse(_sound.Played);
        }

        [TestMethod]
        public void BasicAlarm_TurnOff_TurnsOffSound()
        {
            _sut.Arm();
            _sut.Trigger(new XY());

            _sut.TurnOff();

            Assert.IsTrue(_sound.Stopped);
        }
    }
}
