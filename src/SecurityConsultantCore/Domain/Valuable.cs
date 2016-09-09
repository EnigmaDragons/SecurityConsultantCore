using System;

namespace SecurityConsultantCore.Domain
{
    public class Valuable : IValuable
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = "Unnamed";
        public string Type { get; set; } = "None";
        public int Value { get; set; } = 0;
        public Publicity Publicity { get; set; } = Publicity.High;
        public Liquidity Liquidity { get; set; } = Liquidity.Low;
        public string[] Traits { get; set; } = new string[0];

        protected bool Equals(Valuable other)
        {
            return string.Equals(Id, other.Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Valuable) obj);
        }

        public override int GetHashCode()
        {
            return Id?.GetHashCode() ?? 0;
        }
    }
}