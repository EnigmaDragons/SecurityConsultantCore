using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurityConsultantCore.Domain;

namespace SecurityConsultantCore.Test.Domain
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ValuablesContainerTests
    {
        private ValuablesContainer _container;
        private IValuable _sampleValuable;

        [TestInitialize]
        public void Init()
        {
            _container = new ValuablesContainer();
            _sampleValuable = new Valuable();
        }

        [TestMethod]
        public void ValuablesContainer_CanPutValuablesInContainer_HasValuable()
        {
            _container.Put(_sampleValuable);

            Assert.AreEqual(1, _container.Valuables.Count());
            Assert.IsTrue(_container.Valuables.Contains(_sampleValuable));
        }

        [TestMethod]
        public void ValuablesContainer_CanTakeValuableFromContainer_CorrectValuableRemoved()
        {
            _container.Put(_sampleValuable);
            _container.Put(new Valuable());
            _container.Remove(_sampleValuable);

            Assert.AreEqual(1, _container.Valuables.Count());
            Assert.IsFalse(_container.Valuables.Contains(_sampleValuable));
        }
    }
}
