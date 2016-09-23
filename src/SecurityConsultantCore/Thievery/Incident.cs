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

        public double GetTotalItemValue()
        {
            return _allValuables.Sum(v => v.Value);
        }

        public double GetTotalStolenValue()
        {
            return _stolenValuables.Sum(v => v.Value);
        }

        public double GetPercentStolen()
        {
            return (double)_stolenValuables.Count / _allValuables.Count;
        }

        public double GetPercentValueStolen()
        {
            return GetTotalStolenValue() / GetTotalItemValue();
        }

        public void AddStolenItem(Valuable valuable)
        {
            _stolenValuables.Add(valuable);
        }

        public bool IsSuccessful()
        {
            return !GetTotalStolenValue().Equals(0.0) || !GetPercentStolen().Equals(0.0);
        }
    }
}