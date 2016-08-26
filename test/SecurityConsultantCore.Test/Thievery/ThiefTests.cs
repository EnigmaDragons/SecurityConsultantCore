using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTestExtensions;
using SecurityConsultantCore.Domain;
using SecurityConsultantCore.Domain.Basic;
using SecurityConsultantCore.MapGeneration;
using SecurityConsultantCore.Thievery;

namespace SecurityConsultantCore.Test.Thievery
{
    [TestClass]
    public class ThiefTests
    {
        private readonly FacilityMap _map = new FacilityMap();
        private readonly ValuableFacilityObject _upFacingValuable = new ValuableFacilityObject { ObjectLayer = ObjectLayer.UpperObject, Orientation = Orientation.Up };
        private readonly FacilityObject _obstacle = new FacilityObject { ObjectLayer = ObjectLayer.LowerObject, Type = "Not none" };
        private readonly FacilityObject _noGround = new FacilityObject { ObjectLayer = ObjectLayer.Ground, Type = "None" };
        private readonly FacilityObject _noPortal = new FacilityObject { ObjectLayer = ObjectLayer.LowerObject, Type = "None" };
        private readonly FacilityObject _wall = new FacilityObject { ObjectLayer = ObjectLayer.LowerObject, Type = "Wall" };
        private FacilityLayer _layer;
        private Thief _thief;

        [TestInitialize]
        public void Init()
        {
            _thief = new Thief(_map);
            var builder = new LayerBuilder(3, 3);
            builder.PutFloor(new XY(0, 0), new XY(2, 2));
            AddPortals(builder);
            _map.Add(_layer = builder.Build());
        }

        [TestMethod]
        public void Thief_Instructions_IsProperInstruction()
        { 
            var instruction = _thief.Instructions.First(x => { _thief.TravelTo(x.InteractLocation); return true; });

            Assert.IsNotNull(instruction.Action, "Action is null");
            Assert.IsNotNull(instruction.InteractLocation, "Go to location is null");
            Assert.IsNotNull(instruction.Target, "Target is null");
        }

        [TestMethod]
        public void Thief_InstructionWithNoValuables_NoStealInstructionsPresent()
        {
            AssertDoesNotContain(Interactions.Steal);
        }

        [TestMethod]
        public void Thief_StealInstruction_InstructionValid()
        {
           _layer[1, 1].Put(_upFacingValuable);

            var instruction = _thief.Instructions.First(x => { _thief.TravelTo(x.InteractLocation); return x.Action == Interactions.Steal; });

            Assert.AreEqual(new XYZObjectLayer(1, 1, 0, ObjectLayer.UpperObject), instruction.Target);
            Assert.IsTrue(new XY(1, 1).IsAdjacentTo(instruction.InteractLocation));
        }

        [TestMethod]
        public void Thief_StealInstruction_InteractLocationIsOnOpenSpace()
        {
            _layer[1, 0].Put(_obstacle);
            _layer[0, 1].Put(_obstacle);
            _layer[1, 2].Put(_obstacle);
            _layer[1, 1].Put(_upFacingValuable);

            var instruction = _thief.Instructions.First(x => { _thief.TravelTo(x.InteractLocation); return x.Action == Interactions.Steal; });

            Assert.AreEqual(new XYZ(2, 1, 0), instruction.InteractLocation);
        }

        [TestMethod]
        public void Thief_StealInstructionWithValuableOnWall_InteractLocationCorrect()
        {
            _layer[1, 1].Put(_wall);
            _layer[1, 1].Put(_upFacingValuable);

            var instruction = _thief.Instructions.First(x => { _thief.TravelTo(x.InteractLocation); return x.Action == Interactions.Steal; });

            Assert.AreEqual(new XYZ(1, 0, 0), instruction.InteractLocation);
        }

        [TestMethod]
        public void Thief_InstructionWithNoValidStealLocation_NoStealInstructions()
        {
            _layer[1, 0].Put(_noGround);
            _layer[0, 1].Put(_noGround);
            _layer[1, 2].Put(_noGround);
            _layer[2, 1].Put(_noGround);

            _layer[1, 1].Put(_upFacingValuable);

            AssertDoesNotContain(Interactions.Steal);
        }

        [TestMethod]
        public void Thief_InstructionValuableOnWallStealLocationOffTheMap_NoStealInstructions()
        {
            _layer[1, 0].Put(_wall);
            _layer[1, 0].Put(_upFacingValuable);

            AssertDoesNotContain(Interactions.Steal);
        }

        [TestMethod]
        public void Thief_InstructionValuableOnWallNoValidStealLocation_NoStealInstructions()
        {
            _layer[1, 0].Put(_noGround);
            _layer[1, 1].Put(_wall);
            _layer[1, 1].Put(_upFacingValuable);

            AssertDoesNotContain(Interactions.Steal);
        }

        [TestMethod]
        public void Thief_InstructionsWithNoPortal_ThrowsMapException()
        {
            _layer.Portals.ToList().ForEach(x => _layer[x.Location].Put(_noPortal));

            ExceptionAssert.Throws<MapException>(() => _thief.Instructions.First());
        }

        [TestMethod]
        public void Thief_FirstInstruction_ValidEnterInstruction()
        {
            var instruction = _thief.Instructions.First(x => { _thief.TravelTo(x.InteractLocation); return true; });

            Assert.AreEqual(Interactions.Enter, instruction.Action);
            Assert.IsTrue(_map[instruction.Target].Portals.First().IsEdgePortal);
        }

        [TestMethod]
        public void Thief_MultipleItemCapicity_MultipleStealInstructions()
        {
            _thief = new Thief(_map, 2);

            _layer[0, 0].Put(_upFacingValuable);
            _layer[2, 2].Put(_upFacingValuable);

            Assert.AreEqual(2, _thief.Instructions.Count(x => { _thief.TravelTo(x.InteractLocation); return x.Action.Equals(Interactions.Steal); }));
        }

        private void AssertDoesNotContain(string interaction)
        {
            Assert.IsFalse(_thief.Instructions.Any(x => { _thief.TravelTo(x.InteractLocation); return x.Action == interaction; }));
        }

        private void AddPortals(LayerBuilder builder)
        {
            for (var row = 0; row < 3; row++)
                for (var column = 0; column < 3; column++)
                    builder.Put(column, row, new FacilityPortal { ObjectLayer = ObjectLayer.LowerObject, Endpoint1 = SpecialLocation.OffOfMap, Endpoint2 = new XYZ(column, row, 0) });
        }
    }
}
