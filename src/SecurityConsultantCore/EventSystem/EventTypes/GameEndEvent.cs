using SecurityConsultantCore.Thievery;

namespace SecurityConsultantCore.EventSystem.EventTypes
{
    public class GameEndEvent
    {
        public Incidents Incidents { get; private set; }

        public GameEndEvent(Incidents incidents)
        {
            Incidents = incidents;
        }
    }
}
