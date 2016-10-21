using System.Collections.Generic;
using SecurityConsultantCore.Domain;
using SecurityConsultantCore.Domain.Basic;

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
                    { "Floor", new FacilityObject {Type = "Floor", ObjectLayer = ObjectLayer.Ground} },
                    { "Toilet", new FacilityObject {Type = "Toilet", ObjectLayer = ObjectLayer.LowerObject} },
                    { "Sink", new FacilityObject {Type = "Sink", ObjectLayer = ObjectLayer.UpperObject} },
                    { "TowelDispenser", new FacilityObject {Type = "TowelDispenser", ObjectLayer = ObjectLayer.UpperObject} },
                    { "SmallMirror", new FacilityObject {Type = "SmallMirror", ObjectLayer = ObjectLayer.UpperObject} },
                    { "Door", new FacilityPortal {Type = "Door", ObjectLayer = ObjectLayer.UpperObject} },
                    { "Window", new FacilityPortal {Type = "Window", ObjectLayer = ObjectLayer.UpperObject} },
                    { "StairsDown", new FacilityPortal {Type = "StairsDown", ObjectLayer = ObjectLayer.LowerObject} },
                    { "SlopedFloor", new FacilityObject {Type = "SlopedFloor", ObjectLayer = ObjectLayer.Ground} }
                };
            }
        }
    }
}