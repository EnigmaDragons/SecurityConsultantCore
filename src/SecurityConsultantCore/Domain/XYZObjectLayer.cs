using SecurityConsultantCore.Domain.Basic;

namespace SecurityConsultantCore.Domain
{
    public class XYZObjectLayer : XYZW<ObjectLayer>
    {
        public XYZObjectLayer(XYZ xyz, ObjectLayer w) : base(xyz, w) {}

        public XYZObjectLayer(int x, int y, int z, ObjectLayer w) : base(x, y, z, w) {}
    }
}
