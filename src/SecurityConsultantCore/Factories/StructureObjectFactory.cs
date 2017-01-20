using System.Collections.Generic;
using SecurityConsultantCore.Domain;

namespace SecurityConsultantCore.Factories
{
    public class StructureObjectFactory
    {
        private static StructureObjectContainer _container;

        public static FacilityObject Create(string type)
        {
            return GetContainer().Create(type);
        }

        public static List<string> GetConstructables()
        {
            return GetContainer().GetConstructables();
        }

        private static StructureObjectContainer GetContainer()
        {
            return _container ?? (_container = new StructureObjectContainer());
        }

        private class StructureObjectContainer : Container<FacilityObject>
        {
            protected override string GetKey(string id)
            {
                return id;
            }

            protected override Dictionary<string, FacilityObject> GetObjects()
            {
                return new Dictionary<string, FacilityObject>
                {
                    { "Floor", new FacilityObject {Type = "Floor" } },
                    { "Toilet", new FacilityObject {Type = "Toilet" } },
                    { "Sink", new FacilityObject {Type = "Sink" } },
                    { "TowelDispenser", new FacilityObject {Type = "TowelDispenser" } },
                    { "SmallMirror", new FacilityObject {Type = "SmallMirror" } },
                    { "Door", new FacilityPortal {Type = "Door" } },
                    { "Window", new FacilityPortal {Type = "Window" } },
                    { "StairsDown", new FacilityPortal {Type = "StairsDown" } },
                    { "SlopedFloor", new FacilityObject {Type = "SlopedFloor" } }
                };
            }
        }
    }
}