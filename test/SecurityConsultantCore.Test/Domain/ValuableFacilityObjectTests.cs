using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurityConsultantCore.Domain;

namespace SecurityConsultantCore.Test.Domain
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ValuableFacilityObjectTests
    {
        [TestMethod]
        public void ValuableFacilityObject_LinkToOtherValuableFacilityObject_ValuesAndLevelsNowMatch()
        {
            var obj1 = new ValuableFacilityObject {Value = 900, Publicity = Publicity.Confidential, Liquidity = Liquidity.Low};
            var obj2 = new ValuableFacilityObject {Value = 1000, Publicity = Publicity.Obvious, Liquidity = Liquidity.Medium};

            obj1.LinkTo(obj2);

            Assert.AreEqual(obj1.Id, obj2.Id);
            Assert.AreEqual(obj1.Value, obj2.Value);
            Assert.AreEqual(obj1.Publicity, obj2.Publicity);
            Assert.AreEqual(obj1.Liquidity, obj2.Liquidity);
        }

        [TestMethod]
        public void ValuableFacilityObject_LinkToOtherValuableFacilityObjectAsFacilityObjects_ValuesAndLevelsMatch()
        {
            var obj1 = new ValuableFacilityObject { Value = 900, Publicity = Publicity.Confidential, Liquidity = Liquidity.Low };
            var obj2 = new ValuableFacilityObject { Value = 1000, Publicity = Publicity.Obvious, Liquidity = Liquidity.Medium };

            ((FacilityObject)obj1).LinkTo((FacilityObject)obj2);

            Assert.AreEqual(obj1.Id, obj2.Id);
            Assert.AreEqual(obj1.Value, obj2.Value);
            Assert.AreEqual(obj1.Publicity, obj2.Publicity);
            Assert.AreEqual(obj1.Liquidity, obj2.Liquidity);
        }
    }
}
