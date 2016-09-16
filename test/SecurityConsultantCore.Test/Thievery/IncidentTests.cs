using System.Collections.Generic;
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
        public void CalculateTotalItemValue_NoItems_TotalIs0()
        {
            var incident = new Incident(new List<Valuable>());

            double total = incident.CalculateTotalItemValue();

            Assert.AreEqual(0.0, total);
        }

        [TestMethod]
        public void CalculateTotalItemValue_MultipleItems_ExpectedTotalReturned()
        {
            SetupNoStealScenario();

            double total = _incident.CalculateTotalItemValue();

            Assert.AreEqual(300.0, total);
        }

        [TestMethod]
        public void CalculateTotalStolenValue_NoItems_TotalIs0()
        {
            var incident = new Incident(new List<Valuable>());

            double total = incident.CalculateTotalStolenValue();

            Assert.AreEqual(0.0, total);
        }

        [TestMethod]
        public void CalculateTotalStolenValue_MultipleItems_ExpectedTotalReturned()
        {
            SetupStealScenario();

            double total = _incident.CalculateTotalStolenValue();

            Assert.AreEqual(100.0, total);
        }

        [TestMethod]
        public void CalculatePercentStolen_0ItemsAnd0StolenItems_0Returned()
        {
            var incident = new Incident(new List<Valuable> { new Valuable() });

            double percent = incident.CalculatePercentStolen();

            Assert.AreEqual(0.0, percent);
        }

        [TestMethod]
        public void CalculatePercentStolen_MultipleItemsAndStolen_ExpectedResultReturned()
        {
            SetupStealScenario();

            double percent = _incident.CalculatePercentStolen();

            Assert.AreEqual(0.5, percent, 0.01);
        }

        [TestMethod]
        public void CalculatePercentValueStolen_NothingStolen_0Returned()
        {
            SetupNoStealScenario();

            double percent = _incident.CalculatePercentValueStolen();

            Assert.AreEqual(0.0, percent, 0.01);
        }

        [TestMethod]
        public void CalculatePercentValueStolen_OneItemStolen_ExpectedPercentReturned()
        {
            SetupStealScenario();

            double percent = _incident.CalculatePercentValueStolen();

            Assert.AreEqual(0.33, percent, 0.01);
        }

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

        private void SetupStealScenario()
        {
            var valuable1 = new Valuable { Id = "1", Value = 100 };
            var valuable2 = new Valuable { Id = "2", Value = 200 };
            _incident = new Incident(new List<Valuable> { valuable1, valuable2 });
            _incident.AddStolenItem(valuable1);
        }

        private void SetupNoStealScenario()
        {
            var valuable1 = new Valuable { Id = "1", Value = 100 };
            var valuable2 = new Valuable { Id = "2", Value = 200 };
            _incident = new Incident(new List<Valuable> { valuable1, valuable2 });
        }
    }
}