using System;
using System.Collections.Generic;
using System.Linq;
using SecurityConsultantCore.Common;
using SecurityConsultantCore.Domain.Basic;

namespace SecurityConsultantCore.Domain
{
    public class FacilitySpace : ObservedBase<FacilitySpace>
    {
        private static FacilitySpace _emptySpace;
        public static FacilitySpace Empty => _emptySpace ?? (_emptySpace = new FacilitySpace());

        private readonly Dictionary<ObjectLayer, FacilityObject> _layers = new Dictionary<ObjectLayer, FacilityObject>();

        public FacilitySpace()
        {
            foreach (ObjectLayer layer in Enum.GetValues(typeof(ObjectLayer)))
                if (!_layers.ContainsKey(layer))
                    _layers.Add(layer, new FacilityObject());
        }

        public bool IsEmpty => Ground.IsNothing && LowerObject.IsNothing;
        public bool IsOpenSpace => LowerObject.IsNothing && !Ground.IsNothing;

        public FacilityObject Ground { get { return _layers[ObjectLayer.Ground]; } set { Put(ObjectLayer.Ground, value); } }
        public FacilityObject LowerObject { get { return _layers[ObjectLayer.LowerObject]; } set { Put(ObjectLayer.LowerObject, value); } }
        public FacilityObject UpperObject { get { return _layers[ObjectLayer.UpperObject]; } set { Put(ObjectLayer.UpperObject, value); } }
        public FacilityObject Ceiling { get { return _layers[ObjectLayer.Ceiling]; } set { Put(ObjectLayer.Ceiling, value); } }
        public FacilityObject GroundPlaceable { get { return _layers[ObjectLayer.GroundPlaceable]; } set { Put(ObjectLayer.GroundPlaceable, value); } }
        public FacilityObject LowerPlaceable { get { return _layers[ObjectLayer.LowerPlaceable]; } set { Put(ObjectLayer.LowerPlaceable, value); } }
        public FacilityObject UpperPlaceable { get { return _layers[ObjectLayer.UpperPlaceable]; } set { Put(ObjectLayer.UpperPlaceable, value); } }

        public IEnumerable<IValuable> Valuables => GetAll().OfType<IValuable>()
            .Union(GetAll().OfType<ValuablesContainer>().SelectMany(z => z.Valuables));

        public IEnumerable<SecurityObjectBase> Placeables => GetAll().OfType<SecurityObjectBase>();
        public IEnumerable<FacilityPortal> Portals => GetAll().OfType<FacilityPortal>();
        public IEnumerable<ValuablesContainer> FacilityContainers => GetAll().OfType<ValuablesContainer>();
        public IEnumerable<Oriented<IValuable>> OrientedValuables => Valuables.Select(AsOriented);
        public IEnumerable<ValuableFacilityObject> FacilityValuables => GetAll().OfType<ValuableFacilityObject>();

        private Oriented<IValuable> AsOriented(IValuable valuable)
        {
            return new Oriented<IValuable>(
                valuable is FacilityObject ? ((FacilityObject)valuable).Orientation : Orientation.None, valuable);
        }

        public FacilityObject this[ObjectLayer layer] => _layers[layer];

        public List<FacilityObject> GetAll()
        {
            return new List<FacilityObject>
            {
                Ground,
                LowerObject,
                UpperObject,
                Ceiling,
                GroundPlaceable,
                LowerPlaceable,
                UpperPlaceable
            };
        }

        public bool Contains(string objName)
        {
            return _layers.Any(x => x.Value.Type.Contains(objName));
        }

        public bool Contains(FacilityObject obj)
        {
            return _layers.Any(x => x.Value.Equals(obj));
        }

        public FacilityObject Get(string objType)
        {
            return GetAll().Single(x => x.Type.Equals(objType));
        }

        public void Remove(FacilityObject obj)
        {
            if (_layers[obj.ObjectLayer].Type == obj.Type)
                Remove(obj.ObjectLayer);
        }

        public void Remove(IValuable valuable)
        {
            if (valuable is FacilityObject)
                Remove((FacilityObject)valuable);
            else
                GetAll().OfType<ValuablesContainer>().ForEach(y => y.Remove(valuable));
        }

        public void Remove(ObjectLayer layer)
        {
            _layers[layer] = new FacilityObject();
            NotifySubscribers(this);
        }

        public void Put(FacilityObject obj)
        {
            if (obj.ObjectLayer == ObjectLayer.Unknown)
                throw new InvalidOperationException($"Cannot place object with Unknown object layer: '{obj.Type}'");
            Put(obj.ObjectLayer, obj);
        }

        private void Put(ObjectLayer objLayer, FacilityObject obj)
        {
            _layers[objLayer] = obj;
            NotifySubscribers(this);
        }
    }
}
