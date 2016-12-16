namespace SecurityConsultantCore.FacilityObjects
{
    public interface IValuable : IFacilityObject
    {
        string Id { get; }
        string Name { get; }
        int Value { get; }
        Publicity Publicity { get; }
        Liquidity Liquidity { get; }
        string[] Traits { get; }
    }
}
