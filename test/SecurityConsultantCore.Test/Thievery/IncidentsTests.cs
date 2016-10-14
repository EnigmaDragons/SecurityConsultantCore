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
            _incidents = new Incidents(GetTotalValuables());
        }

        [TestMethod]
        public void FailedCount_OneFailedIncident_ExpectedValueReturned()
        {
            var valuable1 = new Valuable { Value = 100 };
            var incident = new Incident();
            incident.AddStolenItem(valuable1);
            _incidents.Add(incident);

            int failed = _incidents.FailedIncidents;

            Assert.AreEqual(0, failed);
        }

        [TestMethod]
        public void FailedAndAttempted_OneOfEach_ExpectedValuesReturned()
        {
            var valuable1 = new Valuable { Value = 100 };
            var incident1 = new Incident();
            var incident2 = new Incident();
            incident1.AddStolenItem(valuable1);
            _incidents.Add(incident1);
            _incidents.Add(incident2);

            int attempted = _incidents.AttemptedIncidents;
            int failed = _incidents.FailedIncidents;

            Assert.AreEqual(2, attempted);
            Assert.AreEqual(1, failed);
        }

        [TestMethod]
        public void GetTotalItemValue_NoValuables_0Returned()
        {
            var incidents = new Incidents(new List<Valuable>());

            Assert.AreEqual(0.0, incidents.GetTotalItemValue());
        }

        [TestMethod]
        public void GetTotalItemValue_Valuables_ExpectedValueReturned()
        {
            Assert.AreEqual(600.0, _incidents.GetTotalItemValue());
        }



        private List<Valuable> GetTotalValuables()
        {
            return new List<Valuable>
            {
                new Valuable { Value = 100 },
                new Valuable { Value = 200 },
                new Valuable { Value = 300 }
            };
        }
    }
}