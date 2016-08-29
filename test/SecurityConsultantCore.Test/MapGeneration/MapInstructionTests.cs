using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurityConsultantCore.MapGeneration;

namespace SecurityConsultantCore.Test.MapGeneration
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class MapInstructionTests
    {
        [TestMethod]
        public void MapInstructionTests_Nothing_IsEmptyMap()
        {
            var map = MapInstruction.FromStrings(CreateLines());

            Assert.AreEqual(0, map.Layers.Count);
            Assert.AreEqual(0, map.Portals.Count);
        }

        [TestMethod]
        public void MapInstructionTests_OneLayer_ContainsOneLayer()
        {
            var map = MapInstruction.FromStrings(CreateLines("Layer: Size=(3,3)"));

            Assert.AreEqual(1, map.Layers.Count);
            Assert.AreEqual(0, map.Portals.Count);
        }

        [TestMethod]
        public void MapInstructionTest_OnePortal_ContainsOnePortal()
        {
            var map = MapInstruction.FromStrings(CreateLines("Portal-Door:(1,1,1);End1=(OffMap);End2=(OffMap)"));

            Assert.AreEqual(0, map.Layers.Count);
            Assert.AreEqual(1, map.Portals.Count);
        }

        private List<string> CreateLines(params string[] args)
        {
            return args.ToList();
        }
    }
}
