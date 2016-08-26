using System;
using SecurityConsultantCore.Common;
using SecurityConsultantCore.Domain;
using SecurityConsultantCore.Domain.Basic;

namespace SecurityConsultantCore.MapGeneration
{
    public class PortalInstruction
    {
        public PortalInstruction(string type, XYZOrientation location, XYZ endpoint1, XYZ endpoint2)
        {
            Type = type;
            Location = location;
            Endpoint1 = endpoint1;
            Endpoint2 = endpoint2;
        }

        public string Type { get; }
        public XYZOrientation Location { get; }
        public XYZ Endpoint1 { get; }
        public XYZ Endpoint2 { get; }

        public static PortalInstruction FromString(string arg)
        {
            if (!arg.Contains("Portal-") || !arg.Contains("End1") || !arg.Contains("End2"))
                throw new ArgumentException($"Invalid Portal Instruction: {arg}");

            var sections = arg.CleanAndSplit(':');
            var type = sections[0].Replace("Portal-", "");
            var props = sections[1].Split(';');
            var location = XYZOrientation.FromString(props[0]);
            var endpoint1 = GetXYZ(props.GetValue("End1"));
            var endpoint2 = GetXYZ(props.GetValue("End2"));

            return new PortalInstruction(type, location, endpoint1, endpoint2);
        }

        private static XYZ GetXYZ(string arg)
        {
            return arg.ToLowerInvariant().Equals("offmap") ? SpecialLocation.OffOfMap : XYZ.FromString(arg);
        }
    }
}