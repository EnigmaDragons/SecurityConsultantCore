using SecurityConsultantCore.FacilityObjects;
using System.Collections.Generic;

namespace SecurityConsultantCore.Thievery
{
    public interface IDesires
    {
        IEnumerable<SpatialValuable> Get();
    }
}
