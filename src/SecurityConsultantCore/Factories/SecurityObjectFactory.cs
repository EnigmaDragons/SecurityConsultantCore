using System.Collections.Generic;
using SecurityConsultantCore.Domain;
using SecurityConsultantCore.Domain.Basic;

namespace SecurityConsultantCore.Factories
{
    public static class SecurityObjectFactory
    {
        private static SecurityObjectContainer _container;

        public static SecurityObject Create(string type)
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

        private class SecurityObjectContainer : Container<SecurityObject>
        {
            protected override string GetKey(string id)
            {
                return id;
            }

            protected override Dictionary<string, SecurityObject> GetObjects()
            {
                return new Dictionary<string, SecurityObject>
                {
                    { "FloorPressurePlate", new SecurityObject { Type = "FloorPressurePlate", Cost = 1000, Traits = new[] {"OpenSpace"} } },
                    { "BatonSecurityGuard", new SecurityObject { Type = "BatonSecurityGuard", Cost = 1500, Traits = new[] {"OpenSpace"} } }
                };
            }
        }
    }
}