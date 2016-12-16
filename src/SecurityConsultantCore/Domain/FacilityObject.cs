using SecurityConsultantCore.Domain.Basic;
using SecurityConsultantCore.FacilityObjects;
using SecurityConsultantCore.Spatial;

namespace SecurityConsultantCore.Domain
{
    public class FacilityObject : IFacilityObject
    {
        public Orientation Orientation { get; set; } = Orientation.Up;
        public Volume Volume { get; set; } = new Volume(new bool[0, 0]);
        public string Type { get; set; } = "None";
        public string Subtype { get; set; } = "None";

        public bool IsNothing => Type.Equals("None");

        public new string ToString()
        {
            return IsNothing ? Type : string.Format("{0} ({1}): {2}", Type, Subtype, Orientation);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            return obj is FacilityObject && Equals((FacilityObject)obj);
        }

        protected bool Equals(FacilityObject other)
        {
            return string.Equals(Type, other.Type)
                   && (Orientation.Equals(other.Orientation))
                   && (Volume.Equals(Volume));
        }
    }
}