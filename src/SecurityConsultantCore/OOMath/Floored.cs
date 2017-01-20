using System;

namespace SecurityConsultantCore.OOMath
{
    public class Floored : Number
    {
        private readonly Number number;

        public Floored(Number number)
        {
            this.number = number;
        }

        public override long AsInt()
        {
            return (long)AsReal();
        }

        public override double AsReal()
        {
            return Math.Floor(number.AsReal());
        }
    }
}
