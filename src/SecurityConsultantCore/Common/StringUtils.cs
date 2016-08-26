using System.Collections.Generic;
using System.Linq;

namespace SecurityConsultantCore.Common
{
    public static class StringUtils
    {
        public static List<string> ToLines(this string arg)
        {
            return arg.Replace("\r", "").Split('\n').ToList();
        }
    }
}