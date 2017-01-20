﻿using SecurityConsultantCore.FacilityObjects;
using SecurityConsultantCore.OOMath;

namespace SecurityConsultantCore.Domain.Basic
{
    public class XYZOriented<T> : XYZOrientation where T : IFacilityObject
    {
        public XYZOriented(XYZOrientation xyzo, T obj) 
            : this(xyzo.X, xyzo.Y, xyzo.Z, xyzo.Orientation, obj)
        {
        }

        public XYZOriented(double x, double y, int z, Orientation w, T obj) : base(x, y, z, w)
        {
            Obj = obj;
        }

        public XYZOriented(Number x, Number y, Number z, Orientation w, T obj) : base(x, y, z, w)
        {
            
        }

        public T Obj { get; }
        public XYZ Location => this;
    }
}
