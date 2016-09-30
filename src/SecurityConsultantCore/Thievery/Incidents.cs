using System.Collections.Generic;
using System.Linq;

namespace SecurityConsultantCore.Thievery
{
    public class Incidents
    {
        private readonly List<Incident> _incidents = new List<Incident>(); 

        public void Add(Incident incident)
        {
            _incidents.Add(incident);
        }

        public int AttemptedIncidents => _incidents.Count;
        public int SuccessfulIncidents => _incidents.Count(i => i.IsSuccessful());
    }
}