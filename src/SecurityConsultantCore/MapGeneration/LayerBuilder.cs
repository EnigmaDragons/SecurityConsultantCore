//using SecurityConsultantCore.Domain;
//using SecurityConsultantCore.Domain.Basic;
//using SecurityConsultantCore.Factories;

//namespace SecurityConsultantCore.MapGeneration
//{
// TODO
//    public class LayerBuilder
//    {
//        private readonly FacilityLayer _layer;
//        private readonly WallGenerator _wallGenerator = new WallGenerator();

//        public LayerBuilder(int width, int height)
//        {
//            //_layer = new FacilityLayer(width, height);
//        }

//        public LayerBuilder(XY size)
//        {
//            //_layer = new FacilityLayer(size.X, size.Y);
//        }

//        public FacilityLayer Build()
//        {
//            _wallGenerator.GenerateWalls(_layer);
//            return _layer;
//        }

//        public void PutFloor(XY xy)
//        {
//            PutFloor(xy.X, xy.Y);
//        }

//        public void PutFloor(double x, double y)
//        {
//            //_layer[x, y].Ground = new FacilityObject {Type = "Floor", ObjectLayer = ObjectLayer.Ground};
//        }

//        public void PutFloor(XY start, XY end)
//        {
//            start.Thru(end).ForEach(PutFloor);
//        }

//        public void Put(double x, double y, FacilityObject obj)
//        {
//            //_layer[x, y].Put(obj);
//        }

//        public void Put(ObjectInstruction inst)
//        {
//            Put(inst, new XY(0, 0));
//        }

//        public void Put(ObjectInstruction inst, XY roomLocation)
//        {
//            var obj = FacilityObjectFactory.Create(inst.ObjectName);
//            obj.Orientation = inst.Location.Orientation;
//            //_layer[roomLocation.Plus(inst.Location)].Put(obj);
//        }

//        public void Link(LinkInstruction linkInstruction)
//        {
//            //var obj1 = _layer[linkInstruction.Obj1.Location].Get(linkInstruction.Obj1.Obj);
//            //var obj2 = _layer[linkInstruction.Obj2.Location].Get(linkInstruction.Obj2.Obj);
//            //obj1.LinkTo(obj2);
//        }

//        public static FacilityLayer Assemble(LayerInstruction instructions)
//        {
//            var builder = new LayerBuilder(instructions.Size);
//            instructions.Rooms
//                .ForEach(x => x.ObjectInstructions
//                    .ForEach(y => builder.Put(y, x.Location)));
//            instructions.Links.ForEach(x => builder.Link(x));
//            return builder.Build();
//        }
//    }
//}