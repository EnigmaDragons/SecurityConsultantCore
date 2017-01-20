namespace SecurityConsultantCore.Domain.Basic
{
    public interface ITyped
    {
        string Type { get; }
        string Subtype { get; }
    }
}