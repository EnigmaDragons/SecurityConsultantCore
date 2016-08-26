using System.Collections.Generic;
using System.Linq;

namespace SecurityConsultantCore.MapGeneration
{
    public static class MapLanguageParsing
    {
        public static string GetValue(this IEnumerable<string> props, string propName)
        {
            return props.Single(x => x.Contains(propName)).Split('=')[1];
        }

        public static string GetValue(this string line, string propName)
        {
            return line.Split('=')[1];
        }
    }
}