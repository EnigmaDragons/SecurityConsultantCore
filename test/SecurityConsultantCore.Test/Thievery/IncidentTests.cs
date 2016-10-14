using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurityConsultantCore.Domain;
using SecurityConsultantCore.Thievery;

namespace SecurityConsultantCore.Test.Thievery
{
    [TestClass, ExcludeFromCodeCoverage]
    public class IncidentTests
    {
        private Incident _incident;

        [TestMethod]
        public void IsSuccessful_NothingStolen_FalseReturned()
        {
            SetupNoStealScenario();

            bool successful = _incident.IsSuccessful();

            Assert.IsFalse(successful);
        }

        [TestMethod]
        public void IsSuccessful_SomethingStolen_TrueReturned()
        {
            SetupStealScenario();

            bool successful = _incident.IsSuccessful();

            Assert.IsTrue(successful);
        }

        [TestMethod]
        public void GetTotalStolenValue_NoItems_TotalIs0()
        {
            var incident = new Incident();

            double total = incident.GetTotalStolenValue();

            Assert.AreEqual(0.0, total);
        }

        [TestMethod]
        public void GetTotalStolenValue_MultipleItems_ExpectedTotalReturned()
        {
            SetupStealScenario();

            double total = _incident.GetTotalStolenValue();

            Assert.AreEqual(100.0, total);
        }

        private void SetupStealScenario()
        {
            var valuable1 = new Valuable { Id = "1", Value = 100 };
            _incident = new Incident();
            _incident.AddStolenItem(valuable1);
        }

        private void SetupNoStealScenario()
        {
            _incident = new Incident();
        }
    }
}