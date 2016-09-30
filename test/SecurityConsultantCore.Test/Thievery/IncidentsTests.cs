using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
        public void SuccessfulCount_OneSuccessful_ExpectedValueReturned()
        {
            var valuable1 = new Valuable { Value = 100 };
            var valuable2 = new Valuable { Value = 200 };
            var incident = new Incident(new List<Valuable> { valuable1, valuable2 });
            incident.AddStolenItem(valuable1);
            _incidents.Add(incident);

            int successful = _incidents.SuccessfulIncidents;

            Assert.AreEqual(1, successful);
        }

        [TestMethod]
        public void SuccessfulAndAttempted_OneOfEach_ExpectedValuesReturned()
        {
            var valuable1 = new Valuable { Value = 100 };
            var valuable2 = new Valuable { Value = 200 };
            var valuable3 = new Valuable { Value = 300 };
            var valuable4 = new Valuable { Value = 400 };
            var incident1 = new Incident(new List<Valuable> { valuable1, valuable2 });
            var incident2 = new Incident(new List<Valuable> { valuable3, valuable4 });
            incident1.AddStolenItem(valuable1);
            _incidents.Add(incident1);
            _incidents.Add(incident2);

            int attempted = _incidents.AttemptedIncidents;
            int successful = _incidents.SuccessfulIncidents;

            Assert.AreEqual(2, attempted);
            Assert.AreEqual(1, successful);
        }
    }
}