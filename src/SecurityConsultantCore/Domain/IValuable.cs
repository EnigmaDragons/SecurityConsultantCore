using SecurityConsultantCore.Domain.Basic;

namespace SecurityConsultantCore.Domain
{
    public interface IValuable : ITyped
    {
        string Id { get; }
        string Name { get; }
        int Value { get; }
        Publicity Publicity { get; }
        Liquidity Liquidity { get; }
        string[] Traits { get; }
    }
}