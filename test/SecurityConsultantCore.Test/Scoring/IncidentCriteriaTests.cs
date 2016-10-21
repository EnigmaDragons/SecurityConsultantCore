using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurityConsultantCore.Scoring;
using SecurityConsultantCore.Scoring.Criteria;

namespace SecurityConsultantCore.Test.Scoring
{
    [TestClass, ExcludeFromCodeCoverage]
    public class IncidentCriteriaTests
    {
        [TestMethod]
        public void GetScore_AllThievesEscapedNoErrorMargin_ZeroReturned()
        {
            AssertScore(100, 100, 0.0, 0.0);
        }

        [TestMethod]
        public void GetScore_HalfThievesLostNoErrorMargin_CorrectScoreReturned()
        {
            AssertScore(50, 100, 0.0, 0.5);
        }

        [TestMethod]
        public void GetScore_10PercentThievesLostNoErrorMargin_CorrectScoreReturned()
        {
            AssertScore(1, 10, 0.0, 0.9);
        }

        [TestMethod]
        public void GetScore_ThreeQuartersThievesLostWith50PercentErrorMargin_CorrectScoreReturned()
        {
            AssertScore(3, 4, 0.5, 0.5);
        }

        [TestMethod]
        public void GetScore_OneQuarterThievesLostWith50PercentErrorMargin_CorrectScoreReturned()
        {
            AssertScore(1, 4, 0.5, 1.0);
        }

        private void AssertScore(int thievesEscaped, int totalThieves, double marginOfError, double expectedScore)
        {
            var criteria = new IncidentCriteria(thievesEscaped, totalThieves, marginOfError);

            var score = criteria.GetScore();

            Assert.AreEqual("Incidents", score.Name);
            Assert.AreEqual(expectedScore, score.Score);
        }
    }
}