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

        public override long AsInt()
        {
            return new SimpleNumber(AsReal()).AsInt();
        }

        public override double AsReal()
        {
            return Math.Abs(number.AsReal());
        }
    }
}
