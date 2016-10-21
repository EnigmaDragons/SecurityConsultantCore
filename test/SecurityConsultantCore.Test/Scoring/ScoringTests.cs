using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurityConsultantCore.Scoring;
using SecurityConsultantCore.Scoring.Criteria;

namespace SecurityConsultantCore.Test.Scoring
{
    [TestClass]
    public class ScoringTests
    {
        private const double _perfectScore = 1.0;
        private const double _delta = 0.01;
        private Score _score;
        private List<IScoringCriteria> _criteria;

        [TestInitialize]
        public void Init()
        {
            _criteria = new List<IScoringCriteria>();
            _score = new Score(_criteria);
        }


        [TestMethod]
        public void Score_NoCriteria_FinalScoreIsPerfect()
        {
            AssertFinalScoreIs(_perfectScore);
        }

        [TestMethod]
        public void Score_NoCriteria_SubScoresCountIsZero()
        {
            var subscores = _score.GetSubScores();

            Assert.AreEqual(0, subscores.Count());
        }

        [TestMethod]
        public void Score_OneCriteria_FinalScoreMatchesCriteriaScore()
        {
            _criteria.Add(new FakeCriteria("Fake", 0.72, 2));

            AssertFinalScoreIs(0.72);
        }

        [TestMethod]
        public void Score_OneCriteria_SubScoresAreCorrect()
        {
            _criteria.Add(new FakeCriteria("Fake", 0.77, 7));

            var subScores = _score.GetSubScores().ToList();

            Assert.AreEqual(1, subScores.Count());
            Assert.AreEqual("Fake", subScores.First().Name);
            Assert.AreEqual(0.77, subScores.First().Score, _delta);
        }

        [TestMethod]
        public void Score_CriteriaAddedAfterCalculation_FinalUnchanged()
        {
            _score.GetFinalScore();
            _criteria.Add(new FakeCriteria("DirtyCheater", 0.1, 10));

            AssertFinalScoreIs(_perfectScore);
        }

        [TestMethod]
        public void Score_CriteriaAddedAfterCalculation_SubScoresUnchanged()
        {
            _score.GetFinalScore();
            _criteria.Add(new FakeCriteria("DirtyCheater", 0.1, 10));

            var subScores = _score.GetSubScores();

            Assert.AreEqual(0, subScores.Count());
        }

        [TestMethod]
        public void Score_ThreeCriteria_FinalScoreIsWeightedComposite()
        {
            _criteria.Add(new FakeCriteria("IsRacist", 0.06, 1));
            _criteria.Add(new FakeCriteria("IsSexist", 0.12, 2));
            _criteria.Add(new FakeCriteria("IsHomophobic", 0.36, 3));

            AssertFinalScoreIs(0.23);
        }

        [TestMethod]
        public void Score_ThreeCriteria_SubScoresAreCorrect()
        {
            _criteria.Add(new FakeCriteria("IsRacist", 0.06, 1));
            _criteria.Add(new FakeCriteria("IsSexist", 0.12, 2));
            _criteria.Add(new FakeCriteria("IsHomophobic", 0.36, 3));

            var subScores = _score.GetSubScores().ToList();

            Assert.AreEqual(3, subScores.Count);
            Assert.AreEqual(1, subScores.Count(s => s.Name.Equals("IsRacist")));
            Assert.AreEqual(1, subScores.Count(s => s.Name.Equals("IsSexist")));
            Assert.AreEqual(1, subScores.Count(s => s.Name.Equals("IsHomophobic")));
        }

        private void AssertFinalScoreIs(double expected)
        {
            Assert.AreEqual(expected, _score.GetFinalScore(), _delta);
        }
    }
}