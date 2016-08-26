using System;
using SecurityConsultantCore.Domain.Basic;

namespace SecurityConsultantCore.Domain
{
    public class FacilityPortal : FacilityObject
    {
        public XYZ Endpoint1 { get; set; } = SpecialLocation.OffOfMap;
        public XYZ Endpoint2 { get; set; } = SpecialLocation.OffOfMap;

        public bool IsEdgePortal
            => (Endpoint1.Equals(SpecialLocation.OffOfMap) && !Endpoint2.Equals(SpecialLocation.OffOfMap)) ||
               (Endpoint2.Equals(SpecialLocation.OffOfMap) && !Endpoint1.Equals(SpecialLocation.OffOfMap));

        public XYZ GetDestination(XYZ source)
        {
            if (source.Equals(Endpoint1))
                return Endpoint2;
            if (source.Equals(Endpoint2))
                return Endpoint1;
            throw new InvalidOperationException("Invalid portal entry location: " + source);
        }

        public static FacilityPortal FromObject(FacilityObject obj)
        {
            return new FacilityPortal {Type = obj.Type, ObjectLayer = obj.ObjectLayer, Orientation = obj.Orientation};
        }
    }
}