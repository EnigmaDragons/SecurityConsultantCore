using System.Collections.Generic;

namespace SecurityConsultantCore.Domain
{
    public interface IPortals
    {
        IEnumerable<FacilityPortal> Portals { get; }
    }
}
