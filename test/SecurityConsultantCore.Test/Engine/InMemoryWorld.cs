﻿using System.Collections.Generic;
using System.Linq;
using SecurityConsultantCore.Domain;
using SecurityConsultantCore.Domain.Basic;
using SecurityConsultantCore.Engine;

namespace SecurityConsultantCore.Test.Engine
{
    public class InMemoryWorld : IWorld
    {
        private Dictionary<XYZ, List<FacilityObject>> _objects = new Dictionary<XYZ, List<FacilityObject>>();

        public void HideEverything()
        {
            _objects.Clear();
        }

        public void Show(FacilitySpace space, XYZ location)
        {
            if (!_objects.ContainsKey(location))
                _objects[location] = new List<FacilityObject>();
            space.GetAll().ForEach(x => _objects[location].Add(x));
        }

        public FacilityObject ObjectAt(XYZ location, ObjectLayer layer)
        {
            if (!_objects.ContainsKey(location))
                return new FacilityObject();
            return _objects[location].First(x => x.ObjectLayer == layer);
        }
    }
}
