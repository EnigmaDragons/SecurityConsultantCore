using System;

namespace SecurityConsultantCore.Common
{
    public static class MathExt
    {
        private static double epsilon = 0.00001;
        public static bool WithinEpsilonOf(this double value1, double value2)
        {
            return Math.Abs(value1 - value2) < epsilon;
        }
    }
}
