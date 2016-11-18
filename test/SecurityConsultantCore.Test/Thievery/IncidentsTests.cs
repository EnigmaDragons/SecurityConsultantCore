using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurityConsultantCore.Domain;
using SecurityConsultantCore.EventSystem;
using SecurityConsultantCore.EventSystem.EventTypes;
using SecurityConsultantCore.Test.EngineMocks;
using SecurityConsultantCore.Test._TestDoubles;
using SecurityConsultantCore.Thievery;

namespace SecurityConsultantCore.Test.Thievery
{
    [TestClass, ExcludeFromCodeCoverage]
    public class IncidentsTests
    {
        private List<IValuable> _mapValuables;
        private Incidents _incidents;
        private IEvents _eventNotification;
        private ThiefTeamFactoryStub _thiefTeamFactory;
        private bool _gameIsOver;

        [TestInitialize]
        public void Init()
        {
            _mapValuables = new List<IValuable>();
            _eventNotification = new Events();
            _thiefTeamFactory = new ThiefTeamFactoryStub(new SingleMemberThiefTeam(new FacilityMap(new InMemoryWorld())));
            _incidents = new Incidents(_mapValuables, _eventNotification, _thiefTeamFactory);
            _eventNotification.Subscribe<GameEndEvent>(OnGameEnd);
        }

        [TestMethod]
        public void Incidents_ThiefTeamStoleNothing_OneFailedIncident()
        {
            _incidents.Update(CreateThiefTeam());

            var failed = _incidents.FailedIncidents;

            Assert.AreEqual(1, failed);
        }

        [TestMethod]
        public void Incidents_OneTeamSucceededAndOneFailed_SummaryCorrect()
        {
            var incident1 = CreateThiefTeam();
            incident1.Update(new List<IValuable> { new Valuable { Value = 100 } });
            _incidents.Update(incident1);
            _incidents.Update(CreateThiefTeam());

            var attempted = _incidents.AttemptedIncidents;
            var failed = _incidents.FailedIncidents;

            Assert.AreEqual(2, attempted);
            Assert.AreEqual(1, failed);
        }

        [TestMethod]
        public void Incidents_NoValuables_ZeroTotalItemValue()
        {
            Assert.AreEqual(0.0, _incidents.GetTotalItemValue());
        }

        [TestMethod]
        public void Incidents_Valuables_ExpectedValueReturned()
        {
            _mapValuables.Add(new Valuable { Value = 100 });
            _mapValuables.Add(new Valuable { Value = 200 });
            _mapValuables.Add(new Valuable { Value = 300 });

            Assert.AreEqual(600.0, _incidents.GetTotalItemValue());
        }

        [TestMethod]
        public void Incidents_OnGameStartEvent_SomeNumberOfThiefTeamsAreCreated()
        {
            _eventNotification.Publish(new GameStartEvent());

            Assert.IsTrue(_thiefTeamFactory.NumberCreated > 0);
        }

        [TestMethod]
        public void Incidents_OnThiefTeamUpdate_IncidentsAreFinished()
        {
            _incidents.Update(CreateThiefTeam());

            Assert.IsTrue(_gameIsOver);
        }

        private void OnGameEnd(GameEndEvent gameEnd)
        {
            _gameIsOver = true;
        }

        private ThiefTeam CreateThiefTeam()
        {
            return new SingleMemberThiefTeam(new FacilityMap(new InMemoryWorld()));
        }
    }
}