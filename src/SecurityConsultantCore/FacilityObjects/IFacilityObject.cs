using SecurityConsultantCore.Domain.Basic;
using SecurityConsultantCore.Spatial;

namespace SecurityConsultantCore.FacilityObjects
{
    public interface IFacilityObject : ITyped
    {
        Orientation Orientation { get; set; }
        Volume Volume { get; set; }
        bool IsNothing { get; }
    }
}
