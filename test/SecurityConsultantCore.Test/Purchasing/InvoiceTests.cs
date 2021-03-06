﻿using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurityConsultantCore.Business;
using SecurityConsultantCore.Domain;
using SecurityConsultantCore.Test._TestDoubles;

namespace SecurityConsultantCore.Test.Purchasing
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class InvoiceTests
    {
        [TestMethod]
        public void CalculateTotal_EmptyCollection_0Returned()
        {
            var invoice = new Invoice(new List<SecurityObjectBase>());

            int totalCost = invoice.CalculateTotal();

            Assert.AreEqual(0, totalCost);
        }

        [TestMethod]
        public void CalculateTotal_CollectionWithSingleItem_ExpectedResultReturned()
        {
            var invoice = new Invoice(new List<SecurityObjectBase> { new FakeSecurityObject { Cost = 1000} });

            int totalCost = invoice.CalculateTotal();

            Assert.AreEqual(1000, totalCost);
        }

        [TestMethod]
        public void CalculateTotal_CollectionWithMultipleEntries_ExpectedResultsReturned()
        {
            var invoice = new Invoice(new List<SecurityObjectBase>
            {
                new FakeSecurityObject { Cost = 1000 },
                new FakeSecurityObject { Cost = 1500 },
                new FakeSecurityObject { Cost = 2000 }
            });

            int totalCost = invoice.CalculateTotal();

            Assert.AreEqual(4500, totalCost);
        }
    }
}