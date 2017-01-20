namespace SecurityConsultantCore.OOMath
{
    public class Negative : Number
    {
        private readonly Number number;

        public Negative(Number number)
        {
            this.number = number;
        }

        public long AsInt()
        {
            return new SimpleNumber(AsReal()).AsInt();
        }

        public double AsReal()
        {
            return -number.AsReal();
        }
    }
}
