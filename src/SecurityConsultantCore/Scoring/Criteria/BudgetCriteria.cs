using System;

namespace SecurityConsultantCore.Scoring.Criteria
{
    public class BudgetCriteria : IScoringCriteria
    {
        private readonly int _budgetUsed;
        private readonly int _totalBudget;
        private readonly double _marginOfError;

        public BudgetCriteria(int budgetUsed, int totalBudget, double marginOfError)
        {
            _budgetUsed = budgetUsed;
            _totalBudget = totalBudget;
            _marginOfError = marginOfError;
        }

        public SubScore GetScore()
        {
            double budgetRemaining = _totalBudget - _budgetUsed;
            double finalScore = budgetRemaining / (_totalBudget - _totalBudget * _marginOfError);
            return new SubScore("TEST", Math.Min(1.0, finalScore), 0);
        }
    }
}