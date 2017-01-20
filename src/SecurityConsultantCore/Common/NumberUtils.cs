
using SecurityConsultantCore.OOMath;

namespace SecurityConsultantCore.Common
{
    public class NumberUtils
    {
        public static Number operator +(Number first, Number other)
        {
            return new Sum(first, other);
        } 
    }
}
