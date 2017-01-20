using System;

namespace SecurityConsultantCore.OOMath
{
    public interface Number
    {
        long AsInt();
        double AsReal();
    }

    public class SimpleNumber : Number
    {
        private readonly double _realValue;

        public SimpleNumber(int value) : this((double)value) { }
        public SimpleNumber(uint value) : this((double)value) { }
        public SimpleNumber(short value) : this((double)value) { }
        public SimpleNumber(ushort value) : this((double)value) { }
        public SimpleNumber(long value) : this((double)value) { }
        public SimpleNumber(decimal value) : this((double)value) { }
        public SimpleNumber(float value) : this((double)value) { }

        public SimpleNumber(double realValue)
        {
            _realValue = realValue;
        }

        public static implicit operator SimpleNumber(int value)
        {
            return new SimpleNumber(value);
        }

        public long AsInt()
        {
            return new Floored(this).AsInt();
        }

        public double AsReal()
        {
            return _realValue;
        }
    }
}
