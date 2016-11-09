using SecurityConsultantCore.Common;
using SecurityConsultantCore.Domain;
using SecurityConsultantCore.Thievery;

namespace SecurityConsultantCore.Test._TestDoubles
{
    public class ThiefTeamFactoryStub : IFactory<ThiefTeam>
    {
        private readonly ThiefTeam _thiefTeam;

        public ThiefTeamFactoryStub(FacilityMap map) 
            : this(new SingleMemberThiefTeam(map)) { }

        public ThiefTeamFactoryStub(ThiefTeam thiefTeam)
        {
            _thiefTeam = thiefTeam;
        }

        public int NumberCreated { get; set; }

        public ThiefTeam Create()
        {
            NumberCreated++;
            return _thiefTeam;
        }
    }
}