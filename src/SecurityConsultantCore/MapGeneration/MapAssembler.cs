using SecurityConsultantCore.Domain;
using SecurityConsultantCore.Factories;

namespace SecurityConsultantCore.MapGeneration
{
    public class MapAssembler
    {
        public static FacilityMap Assemble(MapInstruction inst)
        {
            var map = new FacilityMap();
            inst.Layers.ForEach(x => map.Add(LayerBuilder.Assemble(x)));
            inst.Portals.ForEach(x => AddPortal(x, map));
            return map;
        }

        private static void AddPortal(PortalInstruction portalInstruction, FacilityMap map)
        {
            var obj = FacilityObjectFactory.Create(portalInstruction.Type);
            var portal = obj is FacilityPortal ? obj as FacilityPortal : FacilityPortal.FromObject(obj);
            portal.Endpoint1 = portalInstruction.Endpoint1;
            portal.Endpoint2 = portalInstruction.Endpoint2;
            portal.Orientation = portalInstruction.Location.Orientation;
            map[portalInstruction.Location].Put(portal);
        }
    }
}