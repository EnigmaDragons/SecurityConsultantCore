
namespace SecurityConsultantCore.Thievery
{
    public class PreferenceMostHidden : Preference
    {
        public PreferenceMostHidden() 
            : base((x, y) => -x.Publicity.CompareTo(y.Publicity))
        {
        }
    }
}
