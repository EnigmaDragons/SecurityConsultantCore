using System.Collections.Generic;
using SecurityConsultantCore.Domain;

namespace SecurityConsultantCore.Thievery
{
    public interface IDesires
    {
        IEnumerable<LocatedValuable> Get();
    }
}
