using System;
using System.Collections.Generic;

namespace Lab
{
    public static class MyOwnLing
    {
        public static IEnumerable<TSource> JoeyWhere<TSource>(this List<TSource> sources, Predicate<TSource> predicate)
        {
            var enumerator = sources.GetEnumerator();

            while (enumerator.MoveNext())
            {
                var item = enumerator.Current;

                if (predicate(item))
                {
                    yield return item;
                }
            }
            
            // foreach (var source in sources)
            // {
            //     if (predicate(source))
            //     {
            //         yield return source;
            //     }
            // }
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

        public static IEnumerable<TResult> JoeySelect<TSource, TResult>(this IEnumerable<TSource> sources, Func<TSource, TResult> selector)
        {
            foreach (var source in sources)
            {
                yield return selector(source);
            }
        }

        public static IEnumerable<TResult> JoeySelect<TSource, TResult>(this IEnumerable<TSource> sources, Func<TSource, int, TResult> selector)
        {
            var index = 0;
            foreach (var source in sources)
            {
                yield return selector(source, index);

                index++;
            }
        }
    }
}