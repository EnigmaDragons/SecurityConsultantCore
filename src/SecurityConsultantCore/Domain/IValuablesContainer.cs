using System.Collections.Generic;

namespace SecurityConsultantCore.Domain
{
    public interface IValuablesContainer
    {
        IEnumerable<IValuable> Valuables { get; }
        void Remove(IValuable valuable);
    }
}