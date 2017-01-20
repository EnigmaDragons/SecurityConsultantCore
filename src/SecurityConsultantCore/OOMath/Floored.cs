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

        public long AsInt()
        {
            return (long)AsReal();
        }

        public double AsReal()
        {
            return Math.Floor(number.AsReal());
        }
    }
}
