using System;

namespace SecurityConsultantCore.Scoring.Criteria
{
    public class ValuablesCriteria : IScoringCriteria
    {
        private readonly int _amountLost;
        private readonly int _totalValue;
        private readonly double _marginOfError;

        public ValuablesCriteria(int amountLost, int totalValue, double marginOfError)
        {
            _amountLost = amountLost;
            _totalValue = totalValue;
            _marginOfError = marginOfError;
        }

        public SubScore GetScore()
        {
            double budgetRemaining = _totalValue - _amountLost;
            double finalScore = budgetRemaining / (_totalValue - _totalValue * _marginOfError);
            return new SubScore("Valuables", Math.Min(1.0, finalScore), 0);
        }
    }
}