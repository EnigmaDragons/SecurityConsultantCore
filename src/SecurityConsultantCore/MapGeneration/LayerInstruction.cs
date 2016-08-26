using System;
using System.Collections.Generic;
using System.Linq;
using SecurityConsultantCore.Domain.Basic;

namespace SecurityConsultantCore.MapGeneration
{
    public class LayerInstruction
    {
        public LayerInstruction(XY size, List<RoomInstruction> rooms, List<LinkInstruction> links)
        {
            Size = size;
            Rooms = rooms;
            Links = links;
        }

        public XY Size { get; }
        public List<RoomInstruction> Rooms { get; }
        public List<LinkInstruction> Links { get; }

        public static LayerInstruction FromStrings(List<string> lines)
        {
            if (lines.Count == 0)
                throw new ArgumentException("No placement instructions.");
            if (lines.Count(x => x.Contains("Layer") && x.Contains("Size=")) == 0)
                throw new ArgumentException("Layer size not found.");

            var size = GetSize(lines.Single(x => x.Contains("Layer") && x.Contains("Size=")));

            var rooms = new List<RoomInstruction>();
            var links = lines.Where(x => x.StartsWith("Link:")).Select(GetLinkInstruction).ToList();

            var foundFirstRoom = false;
            var currentLines = new List<string>();
            foreach (var line in lines.Where(x => !x.StartsWith("Link:")))
            {
                if (line.StartsWith("Room"))
                {
                    if (foundFirstRoom)
                        rooms.Add(RoomInstruction.FromStrings(currentLines));
                    else
                        foundFirstRoom = true;
                    currentLines.Clear();
                }
                currentLines.Add(line);
            }
            if (foundFirstRoom)
                rooms.Add(RoomInstruction.FromStrings(currentLines));

            return new LayerInstruction(size, rooms, links);
        }

        private static LinkInstruction GetLinkInstruction(string arg)
        {
            return LinkInstruction.FromString(arg);
        }

        private static XY GetSize(string layerProperties)
        {
            return XY.FromString(layerProperties.GetValue("Size"));
        }
    }
}