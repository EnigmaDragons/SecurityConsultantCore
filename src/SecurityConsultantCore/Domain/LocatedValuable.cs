using SecurityConsultantCore.Domain.Basic;

namespace SecurityConsultantCore.Domain
{
    public class LocatedValuable : XYZLocation<IValuable>
    {
        public LocatedValuable(XYZ location, IValuable obj) : base(location, obj)
        {
        }
    }
}
