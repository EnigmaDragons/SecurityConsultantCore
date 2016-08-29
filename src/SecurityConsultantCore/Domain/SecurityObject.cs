namespace SecurityConsultantCore.Domain
{
    public class SecurityObject : FacilityObject
    {
        public string[] Traits { get; set; } = new string[0];
        public int Cost { get; set; }
    }
}