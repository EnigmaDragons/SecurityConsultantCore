using SecurityConsultantCore.Domain.Basic;

namespace SecurityConsultantCore.Domain
{
    public class XYZObjectLayer : XYZW<ObjectLayer>
    {
        //TODO: super hack
        public XYZObjectLayer(XYZLocation<IValuable> valuable) : this(valuable.Location, ObjectLayer.Ceiling)
        {
        }

        public XYZObjectLayer(XYZLocation<FacilityPortal> portal) : this(portal.Location, portal.Obj.ObjectLayer)
        {
        }

        public XYZObjectLayer(XYZLocation<FacilityObject> obj) : this(obj.Location, obj.Obj.ObjectLayer)
        {
        }

        public XYZObjectLayer(XYZ xyz, ObjectLayer w) : this(xyz.X, xyz.Y, xyz.Z, w)
        {
        }

        public XYZObjectLayer(int x, int y, int z, ObjectLayer w) : base(x, y, z, w)
        {
        }

        public ObjectLayer Layer => W;
    }
}