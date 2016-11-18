using System;

namespace SecurityConsultantCore.Scoring.Criteria
{
    public class IncidentCriteria : IScoringCriteria
    {
        private int _thievesEscaped { get; }
        private int _totalThieves { get; }
        private double _marginOfError { get; }

        public IncidentCriteria(int thievesEscaped, int totalThieves, double marginOfError)
        {
            _thievesEscaped = thievesEscaped;
            _totalThieves = totalThieves;
            _marginOfError = marginOfError;
        }

        public SubScore GetScore()
        {
            double budgetRemaining = _totalThieves - _thievesEscaped;
            double finalScore = budgetRemaining / (_totalThieves - _totalThieves * _marginOfError);
            return new SubScore("Incidents", Math.Min(1.0, finalScore), 0);
        }
    }
}
