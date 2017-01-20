using System;

namespace SecurityConsultantCore.OOMath
{
    public class Difference : Number
    {
        private readonly Number minuend;
        private readonly Number subtrahend;

        public Difference(Number minuend, Number subtrahend)
        {
            this.minuend = minuend;
            this.subtrahend = subtrahend;
        }

        public override long AsInt()
        {
            return new SimpleNumber(AsReal()).AsInt();
        }

        public override double AsReal()
        {
            return new Sum(minuend, new Negative(subtrahend)).AsReal();
        }
    }
}
