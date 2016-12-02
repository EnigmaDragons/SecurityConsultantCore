using SecurityConsultantCore.PlayerCommands;

namespace SecurityConsultantCore.Domain
{
    public abstract class SecurityObjectBase : FacilityObject
    {
        public string[] Traits { get; set; } = new string[0];
        public int Cost { get; set; }

        public abstract void ConsultWith(IEngineer engineer);
    }
}