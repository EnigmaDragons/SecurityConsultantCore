using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurityConsultantCore.Domain;
using SecurityConsultantCore.Factories;
using SecurityConsultantCore.Purchasing;

namespace SecurityConsultantCore.Test.Purchasing
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class InvoiceTests
    {
        [TestMethod]
        public void CalculateTotal_EmptyCollection_0Returned()
        {
            var invoice = new Invoice(new List<SecurityObject>());

            int totalCost = invoice.CalculateTotal();

            Assert.AreEqual(0, totalCost);
        }

        [TestMethod]
        public void CalculateTotal_CollectionWithPressurePlate_1000Returned()
        {
            var pressurePlate = SecurityObjectFactory.Create("FloorPressurePlate");
            var invoice = new Invoice(new List<SecurityObject> { pressurePlate });

            int totalCost = invoice.CalculateTotal();

            Assert.AreEqual(1000, totalCost);
        }

        [TestMethod]
        public void CalculateTotal_CollectionWithMultipleEntries_ExpectedResultsReturned()
        {
            var pressurePlate = SecurityObjectFactory.Create("FloorPressurePlate");
            var guard1 = SecurityObjectFactory.Create("BatonSecurityGuard");
            var guard2 = SecurityObjectFactory.Create("BatonSecurityGuard");
            var invoice = new Invoice(new List<SecurityObject> { pressurePlate, guard1, guard2 });

            int totalCost = invoice.CalculateTotal();

            Assert.AreEqual(4000, totalCost);
        }
    }
}