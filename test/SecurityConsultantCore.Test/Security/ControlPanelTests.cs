using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurityConsultantCore.Security;
using System.Diagnostics.CodeAnalysis;

namespace SecurityConsultantCore.Test.Security
{
    [TestClass, ExcludeFromCodeCoverage]
    public class ControlPanelTests
    {
        [TestMethod]
        public void ControlPanel_WiredItems_ArmedByPanel()
        {
            var sut = new ControlPanel();
            var armable = new ArmableMock();
            sut.WireComponent(armable);

            sut.ArmComponents();

            Assert.IsTrue(armable.IsArmed);
        }

        [TestMethod]
        public void ControlPanel_WiredItems_DisarmedByPanel()
        {
            var sut = new ControlPanel();
            var armable = new ArmableMock();
            sut.WireComponent(armable);
            armable.Arm();

            sut.DisarmComponents();

            Assert.IsFalse(armable.IsArmed);
        }

        [TestMethod]
        public void ControlPanel_RemovedItems_NoLongerAffectedByPanel()
        {
            var sut = new ControlPanel();
            var armable = new ArmableMock();
            sut.WireComponent(armable);
            sut.ArmComponents();

            sut.RemoveComponent(armable);
            sut.DisarmComponents();

            Assert.IsTrue(armable.IsArmed);
        }
    }

    public class ArmableMock : IArmable
    {
        public bool IsArmed { get; private set; }

        public void Arm()
        {
            IsArmed = true;
        }

        public void Disarm()
        {
            IsArmed = false;
        }
    }
}
