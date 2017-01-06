using SecurityConsultantCore.Domain.Basic;

namespace SecurityConsultantCore.FacilityObjects
{
    public class SpatialValuable : XYZOriented<IValuable>
    {
        public SpatialValuable(XYZOrientation xyzo, IValuable obj) : base(xyzo, obj)
        {
        }

        public SpatialValuable(XYZ location, Orientation orientation, IValuable obj) 
            : base(new XYZOrientation(location, orientation), obj)
        {
        }
    }
}
