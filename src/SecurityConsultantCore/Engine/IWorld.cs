using SecurityConsultantCore.Domain;
using SecurityConsultantCore.Domain.Basic;

namespace SecurityConsultantCore.Engine
{
    public interface IWorld
    {
        void Show(FacilitySpace space, XYZ location);
        void HideEverything();
    }
}
