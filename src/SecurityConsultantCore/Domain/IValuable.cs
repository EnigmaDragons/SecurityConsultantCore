using SecurityConsultantCore.Domain.Basic;

namespace SecurityConsultantCore.Domain
{
    public interface IValuable : ITyped
    {
        string Id { get; }
        string Name { get; }
        int Value { get; }
        int PublicityLevel { get; }
        int LiquidityLevel { get; }
        string[] Traits { get; }
    }
}