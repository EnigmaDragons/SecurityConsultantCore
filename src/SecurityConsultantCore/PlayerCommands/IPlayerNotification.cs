using SecurityConsultantCore.Domain;

namespace SecurityConsultantCore.PlayerCommands
{
    public interface IPlayerNotification
    {
        void Notify(ValuableFacilityObject valuable);
        void Notify(ValuablesContainer container);
    }
}
