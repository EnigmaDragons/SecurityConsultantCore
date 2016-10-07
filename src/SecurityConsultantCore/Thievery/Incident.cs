using System.Collections.Generic;
using System.Linq;
using SecurityConsultantCore.Domain;

namespace SecurityConsultantCore.Thievery
{
    public class Incident
    {
        private readonly List<Valuable> _stolenValuables;

        public Incident()
        {
            _stolenValuables = new List<Valuable>();
        }

        public void AddStolenItem(Valuable valuable)
        {
            _stolenValuables.Add(valuable);
        }

        public bool IsSuccessful()
        {
            return _stolenValuables.Any();
        }

        public double GetTotalStolenValue()
        {
            return _stolenValuables.Sum(v => v.Value);
        }
    }
}