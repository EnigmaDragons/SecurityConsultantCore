using System;
using SecurityConsultantCore.Business;

namespace SecurityConsultantCore.GameState
{
    public class CurrentCompany
    {
        private static Company _company;

        public static Company Get()
        {
            if (_company == null)
                throw new InvalidOperationException("Current company not set.");
            return _company;
        }

        public static void Set(Company company)
        {
            _company = company;
        }
    }
}