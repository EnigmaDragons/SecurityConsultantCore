using System.Collections.Generic;
using SecurityConsultantCore.Domain.Basic;

namespace SecurityConsultantCore.Domain
{
    public class FacilityObject : ITyped
    {
        public Orientation Orientation { get; set; } = Orientation.Up;
        public ObjectLayer ObjectLayer { get; set; } = ObjectLayer.Unknown;
        public List<FacilityObject> LinkedObjs { get; set; } = new List<FacilityObject>();
        public string Type { get; set; } = "None";

        public bool IsNothing => Type.Equals("None");

        public new string ToString()
        {
            return Type.Equals("None") ? Type : Type + ": " + Orientation.ToString();
        }

        public virtual void LinkTo(FacilityObject obj)
        {
            if (ReferenceEquals(obj, this) || LinkedObjs.Contains(obj))
                return;

            LinkedObjs.Add(obj);
            if (!obj.LinkedObjs.Contains(this))
                obj.LinkTo(this);
            LinkedObjs.ForEach(x => x.LinkTo(obj));
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
                   && (Orientation == other.Orientation)
                   && (ObjectLayer == other.ObjectLayer);
        }
    }
}