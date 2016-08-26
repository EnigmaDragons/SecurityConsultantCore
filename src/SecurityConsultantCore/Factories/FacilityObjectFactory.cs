using System;
using System.Collections.Generic;
using SecurityConsultantCore.Domain;

namespace SecurityConsultantCore.Factories
{
    public static class FacilityObjectFactory
    {
        private static Container<Func<FacilityObject>> _container;

        public static FacilityObject Create(string type)
        {
            if (_container == null)
                _container = new FacilityObjectContainer();

            return _container.Create(type).Invoke();
        }

        private class FacilityObjectContainer : Container<Func<FacilityObject>>
        {
            protected override string GetKey(string id)
            {
                return id;
            }

            protected override Dictionary<string, Func<FacilityObject>> GetObjects()
            {
                var aggregateFactory = new Dictionary<string, Func<FacilityObject>>();
                SecurityObjectFactory.GetConstructables()
                    .ForEach(x => aggregateFactory.Add(x, () => SecurityObjectFactory.Create(x)));
                ValuableObjectFactory.GetConstructables()
                    .ForEach(x => aggregateFactory.Add(x, () => ValuableObjectFactory.Create(x)));
                WallFactory.GetConstructables()
                    .ForEach(x => aggregateFactory.Add(x, () => WallFactory.Create(x)));
                StructureObjectFactory.GetConstructables()
                    .ForEach(x => aggregateFactory.Add(x, () => StructureObjectFactory.Create(x)));
                return aggregateFactory;
            }
        }
    }
}