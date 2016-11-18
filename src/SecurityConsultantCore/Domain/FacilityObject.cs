using SecurityConsultantCore.Domain.Basic;

namespace SecurityConsultantCore.Domain
{
    public class FacilityObject : ITyped
    {
        public Orientation Orientation { get; set; } = Orientation.Up;
        public XY Size { get; set; }
        public string Type { get; set; } = "None";

        public bool IsNothing => Type.Equals("None");

        public new string ToString()
        {
            return Type.Equals("None") ? Type : Type + ": " + Orientation.ToString();
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
                   && (Size.Equals(Size));
        }
    }
}