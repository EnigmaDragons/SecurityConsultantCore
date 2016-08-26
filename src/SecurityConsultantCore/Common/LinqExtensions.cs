using System;
using System.Collections.Generic;
using System.Linq;

namespace SecurityConsultantCore.Common
{
    public static class LinqExtensions
    {
        private static Random _rng;

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            var elements = source.ToArray();
            if (elements.Length == 0)
                yield break;
            for (var i = elements.Length - 1; i > 0; i--)
            {
                var swapIndex = GetRandom().Next(i + 1);
                yield return elements[swapIndex];
                elements[swapIndex] = elements[i];
            }
            yield return elements[0];
        }

        private static Random GetRandom()
        {
            return _rng ?? (_rng = new Random(Guid.NewGuid().GetHashCode()));
        }
    }
}