using System;
using System.Collections.Generic;

namespace Lab
{
    public static class MyOwnLing
    {
        public static IEnumerable<TSource> JoeyWhere<TSource>(this List<TSource> sources, Predicate<TSource> predicate)
        {
            foreach (var source in sources)
            {
                if (predicate(source))
                {
                    yield return source;
                }
            }
        }

        public static IEnumerable<TSource> JoeyWhere<TSource>(this List<TSource> sources, Func<TSource, int, bool> predicate)
        {
            var index = 0;
            foreach (var source in sources)
            {
                if (predicate(source, index))
                {
                    yield return source;
                }

                index++;
            }
        }
    }
}