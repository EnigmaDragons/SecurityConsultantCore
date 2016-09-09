using System.Diagnostics;
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
        public void Calculate_InvoiceEqualsBudget_ScoreIs0()
        {
            var score = new Score(1, 1);

            double grade = score.Calculate();

            Assert.AreEqual(0.0, grade);
        }

        [TestMethod]
        public void Calculate_InvoiceHalfBudget_ScoreIs0Point5()
        {
            var score = new Score(1, 0.5);

            double grade = score.Calculate();

            Assert.AreEqual(0.5, grade);
        }

        [TestMethod]
        public void TestMethod()
        {
            WriteToDebug(1.0, 2.0);
            WriteToDebug(1.0, 1.75);
            WriteToDebug(1.0, 1.5);
            WriteToDebug(1.0, 1.25);
            WriteToDebug(1.0, 1.0);
            WriteToDebug(1.0, 0.75);
            WriteToDebug(1.0, 0.5);
            WriteToDebug(1.0, 0.25);
        }

        private void WriteToDebug(double budget, double invoiceTotal)
        {
            double grade = new Score(budget, invoiceTotal).Calculate();
            Debug.WriteLine("budget: {0:0.00}\tinvoice: {1:0.00}\tgrade: {2:0.00}", budget, invoiceTotal, grade);
        }
    }

    // Big time w.i.p.. Currently just calculating % of budget used and flooring at 0.00 if over budget (not too fun)
    // Should also consider details of incident(s) to determine final score. 
    // Eventually, I think the budget/invoice calculation will amount to more of a modifier on the final score
    public class Score
    {
        private readonly double _budget;
        private readonly double _invoiceTotal;

        public Score(double budget, double invoiceTotal)
        {
            _budget = budget;
            _invoiceTotal = invoiceTotal;
        }
        // Target is 0.0 - 1.0
        public double Calculate()
        {
            double result = (_budget - _invoiceTotal) / _budget;
            return result < 0.0 ? 0.00 : result;
        }
    }
}