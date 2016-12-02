using System.Collections.Generic;
using SecurityConsultantCore.Domain;
using SecurityConsultantCore.Domain.Basic;

namespace SecurityConsultantCore.Factories
{
    public static class SecurityObjectFactory
    {
        private static SecurityObjectContainer _container;

        public static SecurityObjectBase Create(string type)
        {
            return GetContainer().Create(type);
        }

        public static List<string> GetConstructables()
        {
            return GetContainer().GetConstructables();
        }

        private static SecurityObjectContainer GetContainer()
        {
            return _container ?? (_container = new SecurityObjectContainer());
        }

        private class SecurityObjectContainer : Container<SecurityObjectBase>
        {
            protected override string GetKey(string id)
            {
                return id;
            }

            protected override Dictionary<string, SecurityObjectBase> GetObjects()
            {
                return new Dictionary<string, SecurityObjectBase> {};
            }
        }
    }
}