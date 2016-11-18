//using System.Diagnostics.CodeAnalysis;
//using System.Linq;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using SecurityConsultantCore.Domain;
//using SecurityConsultantCore.Domain.Basic;
//using SecurityConsultantCore.Test.EngineMocks;
//using SecurityConsultantCore.MapGeneration;

//namespace SecurityConsultantCore.Test.Domain
//{
//    [TestClass]
//    [ExcludeFromCodeCoverage]
//    public class FacilityMapTests
//    {
//        private FacilityMap _map;
//        private FacilityLayer _sampleLayer;
//        private FacilitySpace _sampleSpace;
//        private InMemoryWorld _world;

//        [TestInitialize]
//        public void Init()
//        {
//            _world = new InMemoryWorld();
//            _map = new FacilityMap(_world);
//            _sampleLayer = new FacilityLayer();
//            _sampleSpace = new FacilitySpace();
//        }

//        [TestMethod]
//        public void FacilityMap_AddLayer_CanGetLayer()
//        {
//            _map.Add(_sampleLayer);

//            Assert.AreEqual(_sampleLayer, _map[0]);
//        }

//        [TestMethod]
//        public void FacilityMap_GetFirstLayerSpace_CanGetSpace()
//        {
//            _sampleLayer.Put(1, 2, _sampleSpace);

//            _map.Add(_sampleLayer);

//            Assert.AreEqual(_sampleSpace, _map[1, 2, 0]);
//        }

//        [TestMethod]
//        public void FacilityMap_GetThirdLayerSpace_CanGetSpace()
//        {
//            var thirdFloor = new FacilityLayer();
//            thirdFloor.Put(13, 19, _sampleSpace);

//            _map.Add(_sampleLayer);
//            _map.Add(_sampleLayer);
//            _map.Add(thirdFloor);

//            Assert.AreEqual(_sampleSpace, _map[13, 19, 2]);
//        }

//        [TestMethod]
//        public void FacilityMap_GetSpaceUsingXYZ_CanGetSpace()
//        {
//            _sampleLayer.Put(9, 3, _sampleSpace);

//            _map.Add(_sampleLayer);

//            Assert.AreEqual(_sampleSpace, _map[new XYZ(9, 3, 0)]);
//        }

//        [TestMethod]
//        public void FacilityMap_Valuables_AreCorrect()
//        {
//            _map.Add(_sampleLayer);
//            var value1 = new ValuableFacilityObject { Type = "Painting", ObjectLayer = ObjectLayer.UpperObject };
//            var container = new ValuablesContainer { Type = "Table", ObjectLayer = ObjectLayer.LowerObject };
//            var value2 = new Valuable { Type = "Diamond"};
//            container.Put(value2);
//            _map[5, 5, 0].Put(value1);
//            _map[7, 8, 0].Put(container);

//            var valuables = _map.SpatialValuables.ToList();

//            Assert.AreEqual(2, valuables.Count);
//            Assert.IsTrue(valuables.Any(x => x.Obj.Type.Equals("Painting")));
//            Assert.IsTrue(valuables.Any(x => x.Obj.Type.Equals("Diamond")));
//        }

//        [TestMethod]
//        public void FacilityMap_Portals_AreCorrect()
//        {
//            _map.Add(_sampleLayer);
//            var value1 = new FacilityPortal { Type = "Door", ObjectLayer = ObjectLayer.UpperObject };
//            var value2 = new FacilityPortal { Type = "Window", ObjectLayer = ObjectLayer.UpperObject };
//            _map[5, 5, 0].Put(value1);
//            _map[7, 8, 0].Put(value2);

//            var portals = _map.Portals.ToList();

//            Assert.AreEqual(2, portals.Count());
//            Assert.AreEqual(value1, portals.First(x => x.Location.Equals(new XYZ(5, 5, 0))).Obj);
//            Assert.AreEqual(value2, portals.First(x => x.Location.Equals(new XYZ(7, 8, 0))).Obj);
//        }

//        [TestMethod]
//        public void FacilityMap_RemoveValuable_ValuableNoLongerInSpace()
//        {
//            _map.Add(_sampleLayer);
//            var jewels = new ValuableFacilityObject { Type = "Jewels", ObjectLayer = ObjectLayer.LowerObject };
//            _map[5, 5, 0].Put(jewels);

//            _map.Remove(jewels);

//            Assert.AreEqual(true, _map[5, 5, 0].IsEmpty);
//        }

//        [TestMethod]
//        public void FacilityMap_Exists_ReturnsFalse()
//        {
//            _sampleLayer.Put(1, 1, _sampleSpace);

//            _map.Add(_sampleLayer);

//            Assert.IsFalse(_map.Exists(new XYZ(1, 1, 2)));
//        }

//        [TestMethod]
//        public void FacilityMap_Exist_ReturnsTrue()
//        {
//            _sampleLayer.Put(1, 1, _sampleSpace);

//            _map.Add(_sampleLayer);

//            Assert.IsFalse(_map.Exists(new XYZ(1, 1, 1)));
//        }

//        [TestMethod]
//        public void FacilityMap_EmptyMapInstruction_EmptyMapReturned()
//        {
//            var map = new FacilityMap(_world, CreateInstruction());

//            Assert.AreEqual(0, map.LayerCount);
//        }

//        [TestMethod]
//        public void FacilityMap_SinglePortalMapInstruction_LayersAndPortalsCorrect()
//        {
//            var map = new FacilityMap(_world, CreateInstruction("Layer:Size=1,1", "Portal-Door:0,0,0;End1=(OffMap);End2=(OffMap)"));

//            Assert.AreEqual(1, map.LayerCount);
//            Assert.AreEqual(1, map.Portals.Count());
//        }

//        [TestMethod]
//        public void FacilityMap_WhenShowingLayer_AllObjectsInWorld()
//        {
//            var floor = CreateFloor();
//            _sampleSpace.Put(floor);
//            _sampleLayer.Put(1, 1, _sampleSpace);
//            _map.Add(_sampleLayer);

//            _map.ShowLayer(0);

//            Assert.AreEqual(floor, _world.ObjectAt(new XYZ(1, 1, 0), ObjectLayer.Ground));
//        }

//        [TestMethod]
//        public void FacilityMap_WhenShowingLayerTwoObjects_AllObjectsInWorld()
//        {
//            var floor = CreateFloor();
//            var vase = new FacilityObject { ObjectLayer = ObjectLayer.LowerObject, Type = "Vase" };
//            _sampleSpace.Put(floor);
//            _sampleSpace.Put(vase);
//            _sampleLayer.Put(1, 1, _sampleSpace);
//            _map.Add(_sampleLayer);

//            _map.ShowLayer(0);

//            Assert.AreEqual(floor, _world.ObjectAt(new XYZ(1, 1, 0), ObjectLayer.Ground));
//            Assert.AreEqual(vase, _world.ObjectAt(new XYZ(1, 1, 0), ObjectLayer.LowerObject));
//        }

//        [TestMethod]
//        public void FacilityMap_WhenShowingSecondLayer_FirstLayerNotVisible()
//        {
//            var floor = CreateFloor();
//            _sampleSpace.Put(floor);
//            _sampleLayer.Put(1, 1, _sampleSpace);
//            _map.Add(_sampleLayer);
//            _map.Add(new FacilityLayer());

//            _map.ShowLayer(0);
//            _map.ShowLayer(1);

//            Assert.AreEqual(new FacilityObject(), _world.ObjectAt(new XYZ(1, 1, 0), ObjectLayer.Ground));
//        }

//        private MapInstruction CreateInstruction(params string[] args)
//        {
//            return MapInstruction.FromStrings(args.ToList());
//        }

//        private FacilityObject CreateFloor()
//        {
//            return new FacilityObject { ObjectLayer = ObjectLayer.Ground, Type = "Floor" };
//        }
//    }
//}
