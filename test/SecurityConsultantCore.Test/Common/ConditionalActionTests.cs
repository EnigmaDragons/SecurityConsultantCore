using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurityConsultantCore.Common;

namespace SecurityConsultantCore.Test.Common
{
    [TestClass]
    public class ConditionalActionTests
    {
        private bool succeeded = false;

        [TestMethod]
        public void ConditionalAction_InvokesTrueAction()
        {
            new ConditionalAction(() => true, () => succeeded = true, () => succeeded = false).Invoke();
            Assert.IsTrue(succeeded);
        }

        [TestMethod]
        public void ConditionalAction_InvokesFalseAction()
        {
            new ConditionalAction(() => false, () => succeeded = false, () => succeeded = true).Invoke();
            Assert.IsTrue(succeeded);
        }


    }
}
