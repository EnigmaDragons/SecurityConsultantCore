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
            AssertScore(1, 1, 0.0, 0.0);
        }

        [TestMethod]
        public void GetScore_HalfBudgetUsedNoErrorMargin_CorrectScoreReturned()
        {
            AssertScore(1, 2, 0.0, 0.5);
        }

        [TestMethod]
        public void GetScore_10PercentBudgetUsedNoErrorMargin_CorrectScoreReturned()
        {
            AssertScore(1, 10, 0.0, 0.9);
        }

        [TestMethod]
        public void GetScore_ThreeQuartersBudgetUsedWith50PercentErrorMargin_CorrectScoreReturned()
        {
            AssertScore(3, 4, 0.5, 0.5);
        }

        [TestMethod]
        public void GetScore_OneQuarterBudgetUsedWith50PercentErrorMargin_CorrectScoreReturned()
        {
            AssertScore(1, 4, 0.5, 1.0);
        }

        private void AssertScore(int budgetUsed, int totalBudget, double marginOfError, double expectedScore)
        {
            var criteria = new BudgetCriteria(budgetUsed, totalBudget, marginOfError);

            var score = criteria.GetScore();

            Assert.AreEqual("Budget", score.Name);
            Assert.AreEqual(expectedScore, score.Score);
        }
    }
}