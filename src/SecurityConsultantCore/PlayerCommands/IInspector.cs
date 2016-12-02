using SecurityConsultantCore.Domain;

namespace SecurityConsultantCore.PlayerCommands
{
    public interface IInspector
    {
        void Notify(ValuableFacilityObject valuable);
        void Notify(ValuablesContainer container);
    }
}
