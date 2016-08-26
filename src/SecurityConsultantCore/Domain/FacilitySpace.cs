using System;
using System.Collections.Generic;
using System.Linq;
using SecurityConsultantCore.Domain.Basic;

namespace SecurityConsultantCore.Domain
{
    public class FacilitySpace : IValuablesContainer
    {
        private static FacilitySpace _emptySpace;

        private readonly Dictionary<ObjectLayer, FacilityObject> _layers = new Dictionary<ObjectLayer, FacilityObject>();

        public FacilitySpace()
        {
            foreach (ObjectLayer layer in Enum.GetValues(typeof(ObjectLayer)))
                if (!_layers.ContainsKey(layer))
                    _layers.Add(layer, new FacilityObject());
        }

        public static FacilitySpace Empty => _emptySpace ?? (_emptySpace = new FacilitySpace());

        public bool IsEmpty => Ground.Type.Equals("None") && LowerObject.Type.Equals("None");

        public FacilityObject Ground
        {
            get { return _layers[ObjectLayer.Ground]; }
            set { Put(ObjectLayer.Ground, value); }
        }

        public FacilityObject LowerObject
        {
            get { return _layers[ObjectLayer.LowerObject]; }
            set { Put(ObjectLayer.LowerObject, value); }
        }

        public FacilityObject UpperObject
        {
            get { return _layers[ObjectLayer.UpperObject]; }
            set { Put(ObjectLayer.UpperObject, value); }
        }

        public FacilityObject Ceiling
        {
            get { return _layers[ObjectLayer.Ceiling]; }
            set { Put(ObjectLayer.Ceiling, value); }
        }

        public FacilityObject GroundPlaceable
        {
            get { return _layers[ObjectLayer.GroundPlaceable]; }
            set { Put(ObjectLayer.GroundPlaceable, value); }
        }

        public FacilityObject LowerPlaceable
        {
            get { return _layers[ObjectLayer.LowerPlaceable]; }
            set { Put(ObjectLayer.LowerPlaceable, value); }
        }

        public FacilityObject UpperPlaceable
        {
            get { return _layers[ObjectLayer.UpperPlaceable]; }
            set { Put(ObjectLayer.UpperPlaceable, value); }
        }

        public IEnumerable<SecurityObject> Placeables => GetAll().Where(x => x is SecurityObject).Cast<SecurityObject>()
            ;

        public IEnumerable<FacilityPortal> Portals => GetAll().Where(x => x is FacilityPortal).Cast<FacilityPortal>();

        public FacilityObject this[ObjectLayer layer] => _layers[layer];

        public IEnumerable<IValuable> Valuables => GetAll().Where(x => x is IValuable).Cast<IValuable>()
            .Union(GetAll()
                .Where(y => y is IValuablesContainer)
                .Cast<IValuablesContainer>()
                .SelectMany(z => z.Valuables));

        public void Remove(IValuable valuable)
        {
            if (valuable is FacilityObject)
                Remove((FacilityObject) valuable);
            else
                GetAll()
                    .Where(x => x is IValuablesContainer).Cast<IValuablesContainer>().ToList()
                    .ForEach(y => y.Remove(valuable));
        }

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

        public void Remove(FacilityObject facilityObject)
        {
            if (_layers[facilityObject.ObjectLayer].Type == facilityObject.Type)
                _layers[facilityObject.ObjectLayer] = new FacilityObject();
        }

        public void Remove(ObjectLayer layer)
        {
            _layers[layer] = new FacilityObject();
        }

        public void Put(FacilityObject facilityObject)
        {
            if (facilityObject.ObjectLayer == ObjectLayer.Unknown)
                throw new InvalidOperationException(
                    $"Cannot place object with Unknown object layer: '{facilityObject.Type}'");
            Put(facilityObject.ObjectLayer, facilityObject);
        }

        public bool IsOpenSpace()
        {
            return LowerObject.Type.Equals("None") && !Ground.Type.Equals("None");
        }

        private void Put(ObjectLayer objLayer, FacilityObject obj)
        {
            if (!_layers.ContainsKey(objLayer))
                _layers.Add(objLayer, obj);
            else
                _layers[objLayer] = obj;
        }

        public FacilityObject Get(string objType)
        {
            return GetAll().Single(x => x.Type.Equals(objType));
        }
    }
}