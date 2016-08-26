using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurityConsultant.Code.Thievery;
using SecurityConsultantCore.Domain;
using SecurityConsultantCore.Domain.Basic;
using SecurityConsultantCore.MapGeneration;
using SecurityConsultantCore.Pathfinding;
using SecurityConsultantCore.Thievery;

namespace SecurityConsultantCore.Test.Thievery
{
    [TestClass]
    public class ThiefTests : IThief
    {
        private readonly FacilityMap _map = new FacilityMap();
        private readonly ValuableFacilityObject _upFacingValuable = new ValuableFacilityObject { ObjectLayer = ObjectLayer.UpperObject, Orientation = Orientation.Up, Type = "Unique Name" };
        private readonly ValuableFacilityObject _valuable2 = new ValuableFacilityObject { ObjectLayer = ObjectLayer.UpperObject, Orientation = Orientation.Up, Type = "Unique Name2" };
        private readonly FacilityObject _obstacle = new FacilityObject { ObjectLayer = ObjectLayer.LowerObject, Type = "Not none" };
        private readonly FacilityObject _noGround = new FacilityObject { ObjectLayer = ObjectLayer.Ground, Type = "None" };
        private readonly FacilityObject _noPortal = new FacilityObject { ObjectLayer = ObjectLayer.LowerObject, Type = "None" };
        private readonly FacilityObject _wall = new FacilityObject { ObjectLayer = ObjectLayer.LowerObject, Type = "Wall" };
        private readonly ValuablesContainer _upFacingContainer = new ValuablesContainer { ObjectLayer = ObjectLayer.UpperObject, Orientation = Orientation.Up, Type = "Container" };
        private FacilityLayer _layer;
        private Thief _thief;

        private bool _exited = false;
        private List<Path> _traversedPaths = new List<Path>();
        private List<XYZObjectLayer> _stolenLocations = new List<XYZObjectLayer>();

        [TestInitialize]
        public void Init()
        {
            _thief = new Thief(this, _map);
            var builder = new LayerBuilder(3, 3);
            builder.PutFloor(new XY(0, 0), new XY(2, 2));
            AddPortals(builder);
            _map.Add(_layer = builder.Build());
            _upFacingContainer.Put(_upFacingValuable);
        }

        [TestMethod]
        public void Thief_Go_ExitCalled()
        { 
            _thief.Go();

            Assert.IsTrue(_exited);
        }

        [TestMethod]
        public void Thief_GoWithValuable_TraversedToStealable()
        {
           _layer[1, 1].Put(_upFacingValuable);

            _thief.Go();

            Assert.IsTrue(_traversedPaths.Any(x => x.Last().IsAdjacentTo(new XY(1, 1))));
        }

        [TestMethod]
        public void Thief_StealInstruction_TraversedToOpenSpace()
        {
            _layer[1, 0].Put(_obstacle);
            _layer[0, 1].Put(_obstacle);
            _layer[1, 2].Put(_obstacle);
            _layer[1, 1].Put(_upFacingValuable);

            _thief.Go();

            Assert.AreEqual(new XYZ(2, 1, 0), _traversedPaths.First().Last());
        }

        [TestMethod]
        public void Thief_GoWithValuableOnWall_TraverseLocationCorrect()
        {
            _layer[1, 1].Put(_wall);
            _layer[1, 1].Put(_upFacingValuable);

            _thief.Go();

            Assert.AreEqual(new XYZ(1, 0, 0), _traversedPaths.First().Last());
        }

        [TestMethod]
        public void Thief_GoWithNoValidStealLocation_NoStealCalled()
        {
            _layer[1, 0].Put(_noGround);
            _layer[0, 1].Put(_noGround);
            _layer[1, 2].Put(_noGround);
            _layer[2, 1].Put(_noGround);
            _layer[1, 1].Put(_upFacingValuable);

            _thief.Go();

            Assert.IsFalse(_stolenLocations.Any());
        }

        [TestMethod]
        public void Thief_GoValuableOnWallStealLocationOffTheMap_NoStolenObjects()
        {
            _layer[1, 0].Put(_wall);
            _layer[1, 0].Put(_upFacingValuable);

            _thief.Go();

            Assert.IsFalse(_stolenLocations.Any());
        }

        [TestMethod]
        public void Thief_GoValuableOnWallNoValidStealLocation_NoStolenObjects()
        {
            _layer[1, 0].Put(_noGround);
            _layer[1, 1].Put(_wall);
            _layer[1, 1].Put(_upFacingValuable);

            _thief.Go();

            Assert.IsFalse(_stolenLocations.Any());
        }

        [TestMethod]
        public void Thief_MultipleItemCapicity_MultipleDifferentStolenObjects()
        {
            _thief = new Thief(this, _map, 2);
            _layer[0, 0].Put(_upFacingValuable);
            _layer[2, 2].Put(_valuable2);

            _thief.Go();

            Assert.AreEqual(2, _stolenLocations.Count);
            Assert.AreNotEqual(_stolenLocations.First(), _stolenLocations.Last());
        }

        [TestMethod]
        public void Thief_StealInContainer_TraversedButNoStealLocation()
        {
            _layer[1, 1].Put(_upFacingContainer);

            _thief.Go();

            Assert.IsTrue(_traversedPaths.First().Last().IsAdjacentTo(new XY(1, 1)));
            Assert.AreEqual(0, _stolenLocations.Count);
        }

        [TestMethod]
        public void Thief_StealInContainerOnWall_TraverseToCorrectLocation()
        {
            _layer[1, 1].Put(_upFacingContainer);
            _layer[1, 1].Put(_wall);

            _thief.Go();

            Assert.AreEqual(new XYZ(1, 0, 0),  _traversedPaths.First().Last());
        }

        private void AddPortals(LayerBuilder builder)
        {
            for (var row = 0; row < 3; row++)
                for (var column = 0; column < 3; column++)
                    builder.Put(column, row, new FacilityPortal { ObjectLayer = ObjectLayer.LowerObject, Endpoint1 = SpecialLocation.OffOfMap, Endpoint2 = new XYZ(column, row, 0) });
        }

        public void Traverse(Path path, Action action)
        {
            _traversedPaths.Add(path);
            action.Invoke();
        }

        public void Exit()
        {
            _exited = true;
        }

        public void Steal(XYZObjectLayer valuableLocation)
        {
            _stolenLocations.Add(valuableLocation);
        }
    }
}
