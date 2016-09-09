using System.Collections.Generic;
using System.Linq;
using SecurityConsultantCore.Domain;

namespace SecurityConsultantCore.Thievery
{
    public class Incident
    {
        private readonly List<Valuable> _allValuables;
        private readonly List<Valuable> _stolenValuables;

        public Incident(List<Valuable> allValuables)
        {
            _allValuables = allValuables;
            _stolenValuables = new List<Valuable>();
        }

        public double CalculateTotalItemValue()
        {
            return _allValuables.Sum(v => v.Value);
        }

        public double CalculateTotalStolenValue()
        {
            return _stolenValuables.Sum(v => v.Value);
        }

        public double CalculatePercentStolen()
        {
            return (double)_stolenValuables.Count / _allValuables.Count;
        }

        public double CalculatePercentValueStolen()
        {
            return CalculateTotalStolenValue() / CalculateTotalItemValue();
        }

        public void AddStolenItem(Valuable valuable)
        {
            _stolenValuables.Add(valuable);
        }

        public bool IsSuccessful()
        {
            return !CalculateTotalStolenValue().Equals(0.0) || !CalculatePercentStolen().Equals(0.0);
        }
    }
}