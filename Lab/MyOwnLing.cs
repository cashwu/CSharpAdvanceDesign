using System;
using System.Collections.Generic;

namespace Lab
{
    public static class MyOwnLing
    {
        public static IEnumerable<TSource> JoeyWhere<TSource>(this List<TSource> sources, Func<TSource, bool> predicate)
        {
            foreach (var source in sources)
            {
                if (predicate(source))
                {
                    yield return source;
                }
            }
        }
    }
}