using System.ComponentModel;

namespace SecurityConsultantCore.Domain.Basic
{
    public enum ObjectLayer
    {
        [Description("Unknown")] Unknown,
        [Description("Ground")] Ground,
        [Description("GroundPlaceable")] GroundPlaceable,
        [Description("LowerObject")] LowerObject,
        [Description("LowerPlaceable")] LowerPlaceable,
        [Description("UpperObject")] UpperObject,
        [Description("UpperPlaceable")] UpperPlaceable,
        [Description("Ceiling")] Ceiling
    }
}