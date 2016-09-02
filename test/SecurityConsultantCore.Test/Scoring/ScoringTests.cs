using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SecurityConsultantCore.Test.Scoring
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ScoringTests
    {
        [TestMethod]
        public void Calculate_InvoiceDoublesBudget_ScoreIs0()
        {
            var score = new Score(1, 2);

            double grade = score.Calculate();

            Assert.AreEqual(0.0, grade);
        }

        [TestMethod]
        public void Calculate_InvoiceEqualsBudget_ScoreIs0Point5()
        {
            var score = new Score(1, 1);

            double grade = score.Calculate();

            Assert.AreEqual(0.5, grade);
        }
    }

    public class Score
    {
        private readonly int _budget;
        private readonly int _invoiceTotal;

        public Score(int budget, int invoiceTotal)
        {
            _budget = budget;
            _invoiceTotal = invoiceTotal;
        }

        public double Calculate()
        {
            return (_budget - (double)_invoiceTotal) / _budget;
        }
    }
}