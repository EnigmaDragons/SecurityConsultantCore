using SecurityConsultantCore.Domain;

namespace SecurityConsultantCore.Player
{
    public interface IInspector
    {
        void Notify(ValuableFacilityObject valuable);
        void Notify(ValuablesContainer container);
    }
}
