using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurityConsultantCore.Domain;
using SecurityConsultantCore.Thievery;

namespace SecurityConsultantCore.Test
{
    [TestClass, ExcludeFromCodeCoverage]
    public class IncidentsTests
    {
        private Incidents _incidents;

        [TestInitialize]
        public void Init()
        {
            _incidents = new Incidents();
        }

        [TestMethod]
        public void Add_ValidIncident_AddedToCollection()
        {
            _incidents.Add(new Incident(new List<Valuable>()));

            Assert.AreEqual(1, _incidents.AttemptedIncidents);
        }

        [TestMethod]
        public void SuccessfulCount_ExpectedValueReturned()
        {
            var valuable1 = new Valuable { Value = 100 };
            var valuable2 = new Valuable { Value = 200 };
            var incident = new Incident(new List<Valuable> { valuable1, valuable2 });
            incident.AddStolenItem(valuable1);
            _incidents.Add(incident);

            int successful = _incidents.SuccessfulIncidents;

            Assert.AreEqual(1, successful);
        }
    }

    public class Incidents
    {
        private readonly List<Incident> _incidents = new List<Incident>(); 

        // TODO: Try event stuff
        public void Add(Incident incident)
        {
            _incidents.Add(incident);
        }

        public int AttemptedIncidents => _incidents.Count;
        public int SuccessfulIncidents => _incidents.Count(i => i.IsSuccessful());
    }
}