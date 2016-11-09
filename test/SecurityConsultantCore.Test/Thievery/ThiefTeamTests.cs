using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurityConsultantCore.Domain;
using SecurityConsultantCore.Test.EngineMocks;
using SecurityConsultantCore.Test._TestDoubles;
using SecurityConsultantCore.Thievery;

namespace SecurityConsultantCore.Test.Thievery
{
    [TestClass, ExcludeFromCodeCoverage]
    public class ThiefTeamTests
    {
        private ThiefTeam _team;
        private FacilityMap _map;

        [TestInitialize]
        public void Init()
        {
            _map = new FacilityMap(new InMemoryWorld());
            _team = new SingleMemberThiefTeam(_map);
        }

        [TestMethod]
        public void ThiefTeam_NothingStolen_NotSuccessful()
        {
            var successful = _team.DidYouSucceed();

            Assert.IsFalse(successful);
        }

        [TestMethod]
        public void ThiefTeam_SomethingStolen_IsSuccessful()
        {
           _team.Update(new List<IValuable> { new Valuable() });

            var successful = _team.DidYouSucceed();

            Assert.IsTrue(successful);
        }

        [TestMethod]
        public void ThiefTeam_NoItemsStolen_TotalIsZero()
        {
            var total = _team.HowMuchValueDidYouSteal();

            Assert.AreEqual(0, total);
        }

        [TestMethod]
        public void ThiefTeam_MultipleStolenItems_TotalValueCorrect()
        {
            _team.Update(new List<IValuable> { new Valuable { Value = 54 }, new Valuable { Value = 76 } });

            var total = _team.HowMuchValueDidYouSteal();

            Assert.AreEqual(130, total);
        }
    }
}