using SecurityConsultantCore.Domain;
using SecurityConsultantCore.Domain.Basic;

namespace SecurityConsultantCore.EngineInterfaces
{
    public interface IWorld
    {
        void Show(FacilityObject obj, XYZ location);
        void HideEverything();
    }
}
