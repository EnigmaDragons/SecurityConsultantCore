using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurityConsultantCore.MapGeneration;

namespace SecurityConsultantCore.Test.MapGeneration
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class MapAssemblerTests
    {
        [TestMethod]
        public void MapAssembler_AssembleEmptyMap_EmptyMapReturned()
        {
            var map = MapAssembler.Assemble(Create());

            Assert.AreEqual(0, map.LayerCount);
        }

        [TestMethod]
        public void MapAssembler_AssembleSingleLayerSinglePortalMap_LayersAndPortalsCorrect()
        {
            var map = MapAssembler.Assemble(Create("Layer:Size=1,1", "Portal-Door:0,0,0;End1=(OffMap);End2=(OffMap)"));

            Assert.AreEqual(1, map.LayerCount);
            Assert.AreEqual(1, map.Portals.Count());
        }

        private MapInstruction Create(params string[] args)
        {
            return MapInstruction.FromStrings(args.ToList());
        }
    }
}
