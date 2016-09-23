
namespace SecurityConsultantCore.Thievery
{
    public sealed class PreferenceMostValuable : Preference
    {
        public PreferenceMostValuable() : base((x, y) => x.Value.CompareTo(y.Value))
        {
        }
    }
}
