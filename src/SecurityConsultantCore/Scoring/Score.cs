using System.Collections.Generic;
using System.Linq;
using SecurityConsultantCore.Common;

namespace SecurityConsultantCore.Scoring
{
    public class Score
    {
        private const double _perfectGame = 1.0;
        private readonly Lazy<List<SubScore>> _getSubScores;

        public Score(IEnumerable<IScoringCriteria> criteria) :
            this(new Lazy<List<SubScore>>(() => criteria.Select(c => c.GetScore()).ToList())) {}

        private Score(Lazy<List<SubScore>> getSubScores)
        {
            _getSubScores = getSubScores;
        }

        public double GetFinalScore()
        {
            return !GetSubScores().Any() ? _perfectGame : GetWeightedSubScoreTotal();
        }

        private double GetWeightedSubScoreTotal()
        {
            int totalWeight = GetSubScores().Sum(s => s.Weight);
            double weightedTotal = GetSubScores().Sum(s => s.Weight * s.Score);
            return weightedTotal / totalWeight;
        }

        public IEnumerable<SubScore> GetSubScores()
        {
            return _getSubScores.Get();
        }
    }
}