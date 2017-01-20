using SecurityConsultantCore.Domain.Basic;

namespace SecurityConsultantCore.Spatial
{
    public class XYVolume : XY
    {
        public bool IsSpacious { get; }

        public XYVolume(XY loc) : this(loc.X, loc.Y) { }

        public XYVolume(double x, double y) : this(x, y, true) {}

        public XYVolume(double x, double y, bool hasVolume) : base(x, y)
        {
            IsSpacious = hasVolume;
        }
    }
}
