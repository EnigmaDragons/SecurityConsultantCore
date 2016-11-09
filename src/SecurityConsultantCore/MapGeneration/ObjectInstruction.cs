using System;
using System.Collections.Generic;
using System.Linq;
using SecurityConsultantCore.Common;
using SecurityConsultantCore.Domain.Basic;

namespace SecurityConsultantCore.MapGeneration
{
    public class ObjectInstruction
    {
        public ObjectInstruction(string objectName, XYOrientation placement)
        {
            ObjectName = objectName;
            Location = placement;
        }

        public string ObjectName { get; }
        public XYOrientation Location { get; }

        public override string ToString()
        {
            return $"{ObjectName}: ({Location})";
        }

        protected bool Equals(ObjectInstruction other)
        {
            return string.Equals(ObjectName, other.ObjectName) && Equals(Location, other.Location);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((ObjectInstruction) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((ObjectName?.GetHashCode() ?? 0)*397) ^ (Location?.GetHashCode() ?? 0);
            }
        }

        public static List<ObjectInstruction> FromString(string arg)
        {
            if (string.IsNullOrEmpty(arg) || !arg.Contains(":"))
                throw new ArgumentException(
                    $"Invalid input string: '{arg}'. Expected format: 'objectname: (x, y, orientation)'");

            var parts = arg.CleanAndSplit(':');
            var objectName = parts.First();
            var orientation = GetOrientation(parts);
            var placements = parts.Last().Split(';');

            return CreateInstructions(placements, objectName, orientation);
        }

        private static Orientation GetOrientation(string[] parts)
        {
            return parts.Count() > 2 ? Orientation.FromAbbreviation(parts[1]) : Orientation.None;
        }

        private static List<ObjectInstruction> CreateInstructions(string[] placements, string objectName, Orientation orientation)
        {
            return placements.Where(x => x.Length > 0)
                .SelectMany(x => IsRangeInstruction(x)
                    ? GetRangeInstructions(objectName, x, orientation)
                    : new List<ObjectInstruction> { new ObjectInstruction(objectName, GetXYOrientation(x, orientation))})
                .ToList();
        }

        private static XYOrientation GetXYOrientation(string placement, Orientation defaultOrientation)
        {
            try
            {
                var loc = XYOrientation.FromString(placement);
                return loc.Orientation.Equals(Orientation.None)
                    ? new XYOrientation(loc.X, loc.Y, defaultOrientation)
                    : loc;
            }
            catch (Exception e)
            {
                throw new ArgumentException($"Invalid placement: {placement}", e);
            }
        }

        private static bool IsRangeInstruction(string line)
        {
            return line.Contains("-");
        }

        private static IEnumerable<ObjectInstruction> GetRangeInstructions(string objectName, string arg,
            Orientation orientation)
        {
            var endpoints = arg.Split('-');
            var start = GetXYOrientation(endpoints[0], orientation);
            var end = XY.FromString(endpoints[1]);
            var range = start.Thru(end);
            return range.Select(
                x => new ObjectInstruction(objectName, new XYOrientation(x.X, x.Y, start.Orientation))).ToList();
        }
    }
}