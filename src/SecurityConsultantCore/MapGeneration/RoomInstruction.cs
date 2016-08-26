using System;
using System.Collections.Generic;
using System.Linq;
using SecurityConsultantCore.Domain.Basic;

namespace SecurityConsultantCore.MapGeneration
{
    public class RoomInstruction
    {
        public RoomInstruction(XY roomLocation, List<ObjectInstruction> instructions)
        {
            Location = roomLocation;
            ObjectInstructions = instructions;
        }

        public XY Location { get; }
        public List<ObjectInstruction> ObjectInstructions { get; }

        public static RoomInstruction FromStrings(List<string> lines)
        {
            if (lines.Count == 0)
                throw new ArgumentException("No placement instructions.");
            if (lines.Count(x => x.StartsWith("Room:")) != 1)
                throw new ArgumentException("Single room details not found.");

            var location = XY.FromString(lines.First(x => x.StartsWith("Room:")).Split(':')[1]);
            var instructionLines =
                lines.Where(x => !string.IsNullOrEmpty(x) && !x.Contains("//") && !x.StartsWith("Room:"));
            return new RoomInstruction(location, instructionLines.SelectMany(ObjectInstruction.FromString).ToList());
        }
    }
}