using SecurityConsultantCore.Domain;
using SecurityConsultantCore.Domain.Basic;
using SecurityConsultantCore.EngineInterfaces;
using System.Collections.Generic;
using System.Linq;

namespace SecurityConsultantCore.Test.EngineMocks
{
    public class InMemoryWorld : IWorld
    {
        private Dictionary<XYZOrientation, List<FacilityObject>> _objects = new Dictionary<XYZOrientation, List<FacilityObject>>();

        public void HideEverything()
        {
            _objects.Clear();
        }

        public void Show(XYZOriented<FacilityObject> obj)
        {
            if (!_objects.ContainsKey(obj))
                _objects[obj] = new List<FacilityObject>();
            _objects[obj].Add(obj.Obj);
        }

        public FacilityObject ObjectAt(XYZOrientation location)
        {
            if (!_objects.ContainsKey(location))
                return new FacilityObject();
            return _objects[location].First();
        }
    }
}
