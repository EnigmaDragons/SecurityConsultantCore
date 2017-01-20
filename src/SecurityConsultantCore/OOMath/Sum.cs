namespace SecurityConsultantCore.OOMath
{
    public class Sum : Number
    {
        private readonly Number augend;
        private readonly Number addend;

        public Sum(Number augend, Number addend)
        {
            this.augend = augend;
            this.addend = addend;
        }

        public override long AsInt()
        {
            return new SimpleNumber(AsReal()).AsInt();
        }

        public override double AsReal()
        {
            return augend.AsReal() + addend.AsReal();
        }
    }
}
