using System;
using SecurityConsultantCore.Common;
using SecurityConsultantCore.Domain.Basic;

namespace SecurityConsultantCore.MapGeneration
{
    public class LinkInstruction
    {
        public LinkInstruction(XYLocation<string> obj1, XYLocation<string> obj2)
        {
            Obj1 = obj1;
            Obj2 = obj2;
        }

        public XYLocation<string> Obj1 { get; }
        public XYLocation<string> Obj2 { get; }

        public static LinkInstruction FromString(string arg)
        {
            if (string.IsNullOrEmpty(arg) || !arg.Contains("-"))
                throw new ArgumentException($"Invalid Link instruction: {arg}");

            var targets = arg.Replace("Link:", "").CleanAndSplit('-');
            var obj1Parts = targets[0].Split(',');
            var obj1 = new XYLocation<string>(XY.FromString(obj1Parts[1] + "," + obj1Parts[2]), obj1Parts[0]);
            var obj2Parts = targets[1].Split(',');
            var obj2 = new XYLocation<string>(XY.FromString(obj2Parts[1] + "," + obj2Parts[2]), obj2Parts[0]);
            return new LinkInstruction(obj1, obj2);
        }
    }
}