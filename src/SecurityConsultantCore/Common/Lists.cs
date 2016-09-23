using System.Collections.Generic;

namespace SecurityConsultantCore.Common
{
    public static class Lists
    {
        public static List<T> Of<T>(T item)
        {
            return new List<T> { item };
        }
    }
}
