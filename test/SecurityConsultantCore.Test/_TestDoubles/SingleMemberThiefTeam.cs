using System.Collections.Generic;
using SecurityConsultantCore.Domain;
using SecurityConsultantCore.Thievery;

namespace SecurityConsultantCore.Test._TestDoubles
{
    public class SingleMemberThiefTeam : ThiefTeam
    {
        public SingleMemberThiefTeam(FacilityMap map) : base(new List<Thief> { new Thief(new BodyDummy(), map) })
        {
        }
    }
}