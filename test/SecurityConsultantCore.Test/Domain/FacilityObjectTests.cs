using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurityConsultantCore.Domain;

namespace SecurityConsultantCore.Test.Domain
{
    [TestClass]
    public class FacilityObjectTests
    {
        [TestMethod]
        public void FacilityObject_LinkToSelf_NoLinkFormed()
        {
            var obj1 = new FacilityObject();

            obj1.LinkTo(obj1);

            Assert.AreEqual(0, obj1.LinkedObjs.Count);
        }

        [TestMethod]
        public void FacilityObject_LinkToOtherObj_HasBidirectionalLink()
        {
            var obj1 = new FacilityObject { Type = "Obj1" };
            var obj2 = new FacilityObject { Type = "Obj2" };

            obj1.LinkTo(obj2);

            Assert.IsTrue(obj1.LinkedObjs.Contains(obj2));
            Assert.IsTrue(obj2.LinkedObjs.Contains(obj1));
        }


        [TestMethod]
        public void FacilityObject_ThreeObjectLink_LinksCorrect()
        {
            var obj1 = new FacilityObject { Type = "Obj1" };
            var obj2 = new FacilityObject { Type = "Obj2" };
            var obj3 = new FacilityObject { Type = "Obj3" };

            obj1.LinkTo(obj2);
            obj2.LinkTo(obj3);

            Assert.IsTrue(obj1.LinkedObjs.Contains(obj3));
            Assert.IsTrue(obj3.LinkedObjs.Contains(obj1));
        }
    }
}
