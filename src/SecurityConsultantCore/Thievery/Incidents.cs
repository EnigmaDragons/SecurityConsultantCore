using System.Collections.Generic;
using System.Linq;
using SecurityConsultantCore.Domain;

namespace SecurityConsultantCore.Thievery
{
    public class Incidents
    {
        private readonly List<Incident> _incidents = new List<Incident>(); 
        private readonly List<Valuable> _allValuables;

        public Incidents(List<Valuable> allValuables)
        {
            _allValuables = allValuables;
        }

        public void Add(Incident incident)
        {
            _incidents.Add(incident);
        }

        public int AttemptedIncidents => _incidents.Count;
        public int FailedIncidents => _incidents.Count(i => !i.IsSuccessful());

        public double GetTotalItemValue() => _allValuables.Sum(v => v.Value);

        public double GetPercentValueStolen()
        {
            double totalStolenValue = _incidents.Sum(i => i.GetTotalStolenValue());
            double totalItemValue = _allValuables.Sum(v => v.Value);
            return totalStolenValue / totalItemValue;
        }
    }
}