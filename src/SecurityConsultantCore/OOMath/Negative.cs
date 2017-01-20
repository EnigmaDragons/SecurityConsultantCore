namespace SecurityConsultantCore.OOMath
{
    public class Negative : Number
    {
        private readonly Number number;

        public Negative(Number number)
        {
            this.number = number;
        }

        public override long AsInt()
        {
            return new SimpleNumber(AsReal()).AsInt();
        }

        public override double AsReal()
        {
            return -number.AsReal();
        }
    }
}
