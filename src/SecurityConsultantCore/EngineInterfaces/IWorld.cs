using SecurityConsultantCore.Domain;
using SecurityConsultantCore.Domain.Basic;

namespace SecurityConsultantCore.EngineInterfaces
{
    public interface IWorld
    {
        void Show(XYZOriented<FacilityObject> obj);
        void HideEverything();
    }
}
