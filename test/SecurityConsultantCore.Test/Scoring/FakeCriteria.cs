using SecurityConsultantCore.Scoring;

namespace SecurityConsultantCore.Test.Scoring
{
    public class FakeCriteria : IScoringCriteria
    {
        private readonly double _fixedScoreValue;

        public FakeCriteria(string name, double fixedScoreValue, int criteriaWeight)
        {
            Name = name;
            _fixedScoreValue = fixedScoreValue;
            Weight = criteriaWeight;
        }

        private string Name { get; }
        private int Weight { get; }

        public SubScore GetScore()
        {
            return new SubScore(Name, _fixedScoreValue, Weight);
        }
    }
}