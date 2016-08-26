using System.Collections.Generic;
using System.Linq;

namespace SecurityConsultantCore.MapGeneration
{
    public class MapInstruction
    {
        public MapInstruction(List<LayerInstruction> layers, List<PortalInstruction> portals)
        {
            Layers = layers;
            Portals = portals;
        }

        public List<LayerInstruction> Layers { get; }
        public List<PortalInstruction> Portals { get; }

        public static MapInstruction FromStrings(List<string> lines)
        {
            var portals = lines.Where(x => x.StartsWith("Portal-")).Select(PortalInstruction.FromString).ToList();
            var layers = new List<LayerInstruction>();

            var foundFirstLayer = false;
            var currentLines = new List<string>();
            foreach (var line in lines.Where(x => !x.StartsWith("Portal-")))
            {
                if (line.StartsWith("Layer"))
                {
                    if (foundFirstLayer)
                        layers.Add(LayerInstruction.FromStrings(currentLines));
                    else
                        foundFirstLayer = true;
                    currentLines.Clear();
                }
                currentLines.Add(line);
            }
            if (foundFirstLayer)
                layers.Add(LayerInstruction.FromStrings(currentLines));

            return new MapInstruction(layers, portals);
        }
    }
}