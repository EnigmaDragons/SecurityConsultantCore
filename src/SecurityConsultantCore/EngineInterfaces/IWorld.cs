using SecurityConsultantCore.Domain;
using SecurityConsultantCore.Domain.Basic;

namespace SecurityConsultantCore.EngineInterfaces
{
    public interface IWorld
    {
        void Show(FacilitySpace space, XYZ location);
        void HideEverything();
    }
}
