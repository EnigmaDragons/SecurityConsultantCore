using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTestExtensions;
using SecurityConsultantCore.Domain.Basic;
using SecurityConsultantCore.Pathfinding;

namespace SecurityConsultantCore.Test.Pathfinding
{
    [TestClass, ExcludeFromCodeCoverage]
    public class PatrolRouteTests
    {
        [TestMethod]
        public void PatrolRoute_CreateEmptyRoute_ThrowsOnCreation()
        {
            ExceptionAssert.Throws<ArgumentException>(() => new PatrolRoute());
        }

        [TestMethod]
        public void PatralRoute_SingleSegmentRoutesWithDifferentStartsMatch_False()
        {
            var route1 = new PatrolRoute(new Path(new XYZ(1, 1, 1), new XYZ(2, 2, 2)));
            var route2 = new PatrolRoute(new Path(new XYZ(3, 3, 3), new XYZ(2, 2, 2)));

            Assert.IsFalse(route1.Matches(route2));
        }
    }
}
