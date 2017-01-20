using System;

namespace SecurityConsultantCore.OOMath
{
    public class Absolute : Number
    {
        private readonly Number number;

        public Absolute(Number number)
        {
            this.number = number;
        }

        public long AsInt()
        {
            return new SimpleNumber(AsReal()).AsInt();
        }

        public double AsReal()
        {
            return Math.Abs(number.AsReal());
        }
    }
}
