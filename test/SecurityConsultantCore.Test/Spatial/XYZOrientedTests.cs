using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurityConsultantCore.Domain;
using SecurityConsultantCore.Domain.Basic;
using SecurityConsultantCore.Spatial;

namespace SecurityConsultantCore.Test.Spatial
{
    [TestClass]
    public class XYZOrientedTests
    {
        private XYZOriented<FacilityObject> MakeObject(double x, double y, int z, Orientation o, Volume v)
        {
            return new XYZOriented<FacilityObject>(new XYZOrientation(new XYZ(x, y, z), o), new FacilityObject() { Volume = v });
        }

        [TestMethod]
        public void XYZOriented__()
        {
            var volume = new Volume(new bool[1, 1]);
            var sut = MakeObject(0.0, 0.0, 0, Orientation.Default, volume);


        }
    }
}
