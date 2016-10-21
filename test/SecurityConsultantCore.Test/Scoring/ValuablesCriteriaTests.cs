using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurityConsultantCore.Scoring.Criteria;

namespace SecurityConsultantCore.Test.Scoring
{
    [TestClass, ExcludeFromCodeCoverage]
    public class ValuablesCriteriaTests
    {
        [TestMethod]
        public void GetScore_AllValuablesLostNoErrorMargin_ZeroReturned()
        {
            AssertScore(100, 100, 0.0, 0.0);
        }

        [TestMethod]
        public void GetScore_HalfValuablesLostNoErrorMargin_CorrectScoreReturned()
        {
            AssertScore(50, 100, 0.0, 0.5);
        }

        [TestMethod]
        public void GetScore_10PercentValueLostNoErrorMargin_CorrectScoreReturned()
        {
            AssertScore(1, 10, 0.0, 0.9);
        }

        [TestMethod]
        public void GetScore_ThreeQuartersValueLostWith50PercentErrorMargin_CorrectScoreReturned()
        {
            AssertScore(3, 4, 0.5, 0.5);
        }

        [TestMethod]
        public void GetScore_OneQuarterValueLostWith50PercentErrorMargin_CorrectScoreReturned()
        {
            AssertScore(1, 4, 0.5, 1.0);
        }

        private void AssertScore(int valueLost, int totalValue, double marginOfError, double expectedScore)
        {
            var criteria = new ValuablesCriteria(valueLost, totalValue, marginOfError);

            var score = criteria.GetScore();

            Assert.AreEqual("Valuables", score.Name);
            Assert.AreEqual(expectedScore, score.Score);
        }
    }
}