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
            return new XYZOriented<FacilityObject>(new XYZOrientation(x, y, z, o), new FacilityObject() { Volume = v });
        }
    }
}
