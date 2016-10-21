using SecurityConsultantCore.EngineInterfaces;
using SecurityConsultantCore.Domain;
using SecurityConsultantCore.Domain.Basic;
using System.Collections.Generic;
using System.Linq;

namespace SecurityConsultantCore.Test.EngineMocks
{
    public class InMemoryWorld : IWorld
    {
        private Dictionary<XYZ, List<FacilityObject>> _objects = new Dictionary<XYZ, List<FacilityObject>>();

        public void HideEverything()
        {
            _objects.Clear();
        }

        public void Show(FacilityObject obj, XYZ location)
        {
            if (!_objects.ContainsKey(location))
                _objects[location] = new List<FacilityObject>();
            _objects[location].Add(obj);
        }

        public FacilityObject ObjectAt(XYZ location, ObjectLayer layer)
        {
            if (!_objects.ContainsKey(location))
                return new FacilityObject();
            return _objects[location].First(x => x.ObjectLayer == layer);
        }
    }
}
