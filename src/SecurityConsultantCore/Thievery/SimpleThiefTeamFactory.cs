using System;
using System.Collections.Generic;
using SecurityConsultantCore.Common;
using SecurityConsultantCore.Domain;

namespace SecurityConsultantCore.Thievery
{
    public class SimpleThiefTeamFactory : IFactory<ThiefTeam>
    {
        private readonly Func<IBody> _bodyFactory;
        private readonly FacilityMap _map;

        public SimpleThiefTeamFactory(Func<IBody> bodyFactory, FacilityMap map)
        {
            _bodyFactory = bodyFactory;
            _map = map;
        }

        public ThiefTeam Create()
        {
            return new ThiefTeam(new List<Thief> { new Thief(_bodyFactory(), _map) });
        }
    }
}
