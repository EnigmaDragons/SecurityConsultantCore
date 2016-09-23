using SecurityConsultantCore.Domain.Basic;

namespace SecurityConsultantCore.Domain
{
    public class SpatialValuable : XYZOriented<IValuable>
    {
        public SpatialValuable(XYZ location, Orientation orientation, IValuable obj) 
            : base(new XYZOrientation(location, orientation), obj)
        {
        }
    }
}
