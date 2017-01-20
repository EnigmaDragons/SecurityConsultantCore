using SecurityConsultantCore.Domain;
using System.Collections.Generic;

namespace SecurityConsultantCore.Factories
{
    public class PortalFactory
    {
        private static FacilityPortalContainer _container;

        public static FacilityObject Create(string type)
        {
            return GetContainer().Create(type);
        }

        public static List<string> GetConstructables()
        {
            return GetContainer().GetConstructables();
        }

        private static FacilityPortalContainer GetContainer()
        {
            return _container ?? (_container = new FacilityPortalContainer());
        }

        private class FacilityPortalContainer : Container<FacilityPortal>
        {
            protected override string GetKey(string id)
            {
                return id;
            }

            protected override Dictionary<string, FacilityPortal> GetObjects()
            {
                return new Dictionary<string, FacilityPortal>
                {
                    { "Door", new FacilityPortal {Type = "Door" } },
                    { "Window", new FacilityPortal {Type = "Window" } },
                    { "StairsDown", new FacilityPortal {Type = "StairsDown" } },
                };
            }
        }
    }
}
