using SecurityConsultantCore.Domain;
using SecurityConsultantCore.Domain.Basic;
using SecurityConsultantCore.EngineInterfaces;
using System.Collections.Generic;

namespace SecurityConsultantCore.Test.EngineMocks
{
    public class InMemoryWorld : IWorld
    {
        public Dictionary<XYZ, List<FacilityObject>> Objects { get; } = new Dictionary<XYZ, List<FacilityObject>>();

        public void HideEverything()
        {
            Objects.Clear();
        }

        public void Show(XYZOriented<FacilityObject> obj)
        {
            if (!Objects.ContainsKey(GetCleanKey(obj)))
                Objects[GetCleanKey(obj)] = new List<FacilityObject>();
            Objects[GetCleanKey(obj)].Add(obj.Obj);
        }

        public bool IsObjectAt(FacilityObject obj, XYZ location)
        {
            if (!Objects.ContainsKey(GetCleanKey(location)))
                return false;
            return Objects[GetCleanKey(location)].Contains(obj);
        }

        private XYZ GetCleanKey(XYZ xyz)
        {
            return new XYZ(xyz.X, xyz.Y, xyz.Z);
        }
    }
}
