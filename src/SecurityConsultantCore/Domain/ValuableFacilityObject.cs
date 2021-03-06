﻿using System;

namespace SecurityConsultantCore.Domain
{
    public class ValuableFacilityObject : FacilityObject, IValuable
    {
        public string Id { get; private set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = "Unnamed";
        public int Value { get; set; }
        public Publicity Publicity { get; set; } = Publicity.Famous;
        public Liquidity Liquidity { get; set; } = Liquidity.Low;
        public string[] Traits { get; set; } = new string[0];

        public override void LinkTo(FacilityObject obj)
        {
            if (obj is ValuableFacilityObject)
            {
                LinkTo((ValuableFacilityObject) obj);
                return;
            }

            base.LinkTo(obj);
        }

        public void LinkTo(ValuableFacilityObject obj)
        {
            Id = obj.Id;
            Value = obj.Value;
            Publicity = obj.Publicity;
            Liquidity = obj.Liquidity;
            Name = obj.Name;
            base.LinkTo(obj);
        }

        protected bool Equals(ValuableFacilityObject other)
        {
            return base.Equals(other) && string.Equals(Id, other.Id) && string.Equals(Name, other.Name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((ValuableFacilityObject) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (base.GetHashCode()*397) ^ (Id?.GetHashCode() ?? 0);
            }
        }
    }
}