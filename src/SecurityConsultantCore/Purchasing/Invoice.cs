using System.Collections.Generic;
using System.Linq;
using SecurityConsultantCore.Domain;

namespace SecurityConsultantCore.Purchasing
{
    public class Invoice
    {
        private readonly List<SecurityObject> _securityObjects;

        public Invoice(List<SecurityObject> securityObjects)
        {
            _securityObjects = securityObjects;
        }

        public int CalculateTotal()
        {
            return _securityObjects.Sum(so => so.Cost);
        } 
    }
}