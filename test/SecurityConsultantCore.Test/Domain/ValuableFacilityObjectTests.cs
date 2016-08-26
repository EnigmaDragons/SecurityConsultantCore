using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurityConsultantCore.Domain;

namespace SecurityConsultantCore.Test.Domain
{
    [TestClass]
    public class ValuableFacilityObjectTests
    {
        [TestMethod]
        public void ValuableFacilityObject_LinkToOtherValuableFacilityObject_ValuesAndLevelsNowMatch()
        {
            var obj1 = new ValuableFacilityObject {Value = 900, PublicityLevel = 1, LiquidityLevel = 1};
            var obj2 = new ValuableFacilityObject {Value = 1000, PublicityLevel = 2, LiquidityLevel = 2};

            obj1.LinkTo(obj2);

            Assert.AreEqual(obj1.Id, obj2.Id);
            Assert.AreEqual(obj1.Value, obj2.Value);
            Assert.AreEqual(obj1.PublicityLevel, obj2.PublicityLevel);
            Assert.AreEqual(obj1.LiquidityLevel, obj2.LiquidityLevel);
        }

        [TestMethod]
        public void ValuableFacilityObject_LinkToOtherValuableFacilityObjectAsFacilityObjects_ValuesAndLevelsMatch()
        {
            var obj1 = new ValuableFacilityObject { Value = 900, PublicityLevel = 1, LiquidityLevel = 1 };
            var obj2 = new ValuableFacilityObject { Value = 1000, PublicityLevel = 2, LiquidityLevel = 2 };

            ((FacilityObject)obj1).LinkTo((FacilityObject)obj2);

            Assert.AreEqual(obj1.Id, obj2.Id);
            Assert.AreEqual(obj1.Value, obj2.Value);
            Assert.AreEqual(obj1.PublicityLevel, obj2.PublicityLevel);
            Assert.AreEqual(obj1.LiquidityLevel, obj2.LiquidityLevel);
        }
    }
}
