using System.Collections.Generic;
using System.Linq;
using SecurityConsultantCore.Common;
using SecurityConsultantCore.Domain;

namespace SecurityConsultantCore.Thievery
{
    public class ThiefTeam : ObservedBase<ThiefTeam>, Observer<IEnumerable<IValuable>>
    {
        private readonly List<IValuable> _stolenValuables = new List<IValuable>();
        private readonly List<Thief> _members;

        private int _numThievesActive;

        public ThiefTeam(List<Thief> members)
        {
            _members = members;
        }

        public void Go()
        {
            _numThievesActive = _members.Count;
            _members.ForEach(x => x.Subscribe(this));
            _members.ForEach(x => x.Go());
        }

        public void Update(IEnumerable<IValuable> oneThiefsHaul)
        {
            _stolenValuables.AddRange(oneThiefsHaul);
            _numThievesActive--;
            if (_numThievesActive == 0)
                NotifySubscribers(this);
        }

        public IEnumerable<IValuable> WhatDidYouSteal()
        {
            return _stolenValuables;
        }

        public bool DidYouSucceed()
        {
            return _stolenValuables.Any();
        }

        public int HowMuchValueDidYouSteal()
        {
            return _stolenValuables.Sum(x => x.Value);
        }
    }
}