using System.Collections.Generic;
using System.Linq;
using SecurityConsultantCore.Common;
using SecurityConsultantCore.Domain;
using SecurityConsultantCore.EventSystem;
using SecurityConsultantCore.EventSystem.EventTypes;

namespace SecurityConsultantCore.Thievery
{
    public class Incidents : Observer<ThiefTeam>
    {
        private readonly List<ThiefTeam> _incidents = new List<ThiefTeam>(); 
        private readonly List<IValuable> _allValuables;
        private readonly IEvents _eventNotification;
        private readonly IFactory<ThiefTeam> _thiefTeamFactory;
        
        public Incidents(List<IValuable> allValuables, IEvents eventNotification, IFactory<ThiefTeam> thiefTeamFactory)
        {
            _allValuables = allValuables;
            _eventNotification = eventNotification;
            _thiefTeamFactory = thiefTeamFactory;
            eventNotification.Subscribe<GameStartEvent>(OnGameStart);
        }

        public int AttemptedIncidents => _incidents.Count;
        public int FailedIncidents => _incidents.Count(i => !i.DidYouSucceed());

        public double GetTotalItemValue() => _allValuables.Sum(v => v.Value);

        public double GetPercentValueStolen()
        {
            double totalStolenValue = _incidents.Sum(i => i.HowMuchValueDidYouSteal());
            double totalItemValue = _allValuables.Sum(v => v.Value);
            return totalStolenValue / totalItemValue;
        }

        private void OnGameStart(GameStartEvent gameStart)
        {
            var team = _thiefTeamFactory.Create();
            team.Subscribe(this);
            team.Go();
        }

        public void Update(ThiefTeam team)
        {
            _incidents.Add(team);
            _eventNotification.Publish(new GameEndEvent(this));
        }
    }
}