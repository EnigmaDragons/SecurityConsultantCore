using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using SecurityConsultantCore.Scoring.Criteria;

namespace SecurityConsultantCore.Test.Scoring
{
    [TestClass, ExcludeFromCodeCoverage]
    public class BudgetCriteriaTests
    {

        [TestMethod]
        public void GetScore_AllBudgetUsedWithNoErrorMargin_ZeroReturned()
        {
            var criteria = new BudgetCriteria(1, 1, 0.0);

            var score = criteria.GetScore();

            Assert.AreEqual(0.0, score.Score);
        }

        [TestMethod]
        public void GetScore_HalfBudgetUsedNoErrorMargin_CorrectScoreReturned()
        {
            var criteria = new BudgetCriteria(1, 2, 0.0);

            var score = criteria.GetScore();

            Assert.AreEqual(0.5, score.Score);
        }

        [TestMethod]
        public void GetScore_10PercentBudgetUsedNoErrorMargin_CorrectScoreReturned()
        {
            var criteria = new BudgetCriteria(1, 10, 0.0);

            var score = criteria.GetScore();

            Assert.AreEqual(0.9, score.Score);
        }

        [TestMethod]
        public void GetScore_ThreeQuartersBudgetUsedWith50PercentErrorMargin_CorrectScoreReturned()
        {
            var criteria = new BudgetCriteria(3, 4, 0.5);

            var score = criteria.GetScore();

            Assert.AreEqual(0.5, score.Score);
        }

        [TestMethod]
        public void GetScore_OneQuarterBudgetUsedWith50PercentErrorMargin_CorrectScoreReturned()
        {
            var criteria = new BudgetCriteria(1, 4, 0.5);

            var score = criteria.GetScore();

            Assert.AreEqual(1.0, score.Score);
        }
    }
}