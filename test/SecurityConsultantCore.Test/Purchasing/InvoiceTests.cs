using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurityConsultantCore.Domain;
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
            var invoice = new Invoice(new List<SecurityObject> { new SecurityObject {Cost = 1000} });

            int totalCost = invoice.CalculateTotal();

            Assert.AreEqual(1000, totalCost);
        }

        [TestMethod]
        public void CalculateTotal_CollectionWithMultipleEntries_ExpectedResultsReturned()
        {
            var invoice = new Invoice(new List<SecurityObject>
            {
                new SecurityObject { Cost = 1000 },
                new SecurityObject { Cost = 1500 },
                new SecurityObject { Cost = 2000 }
            });

            int totalCost = invoice.CalculateTotal();

            Assert.AreEqual(4500, totalCost);
        }
    }
}