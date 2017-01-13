using SecurityConsultantCore.Thievery;

namespace SecurityConsultantCore.Event
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
