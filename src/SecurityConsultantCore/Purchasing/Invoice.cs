using System.Collections.Generic;
using System.Linq;
using SecurityConsultantCore.Domain;

namespace SecurityConsultantCore.Purchasing
{
    public class Invoice
    {
        private readonly List<SecurityObjectBase> _securityObjects;

        public Invoice(List<SecurityObjectBase> securityObjects)
        {
            _securityObjects = securityObjects;
        }

        public int CalculateTotal()
        {
            return _securityObjects.Sum(so => so.Cost);
        } 
    }
}