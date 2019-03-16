using System;
using System.Collections.Generic;
using System.Linq;
using Lab.Entities;

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

        public static IEnumerable<Employee> JoeyTake(this IEnumerable<Employee> employees, int takeCount)
        {
            var enumerator = employees.GetEnumerator();

            var index = 0;
            while (enumerator.MoveNext())
            {
                yield return enumerator.Current;
                index++;
                
                if (index == takeCount)
                {
                    yield break;
                }
            }
        }

        public static IEnumerable<TSource> JoeySkip<TSource>(this IEnumerable<TSource> sources, int skipCount)
        {
            var enumerator = sources.GetEnumerator();

            var index = 0;
            while (enumerator.MoveNext())
            {
                if (index >= skipCount)
                {
                    yield return enumerator.Current;
                }

                index++;
            }
        }

        public static bool JoeyAll<TSource>(this IEnumerable<TSource> sources, Func<TSource, bool> predicate)
        {
            var enumerator = sources.GetEnumerator();

            while (enumerator.MoveNext())
            {
                var source = enumerator.Current;
                if (!predicate(source))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool JoeyAny<TSource>(this IEnumerable<TSource> products, Func<TSource, bool> predicate)
        {
            var enumerator = products.GetEnumerator();
            
            while (enumerator.MoveNext())
            {
                var product = enumerator.Current;

                if (predicate(product))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsEmpty<TSource>(this IEnumerable<TSource> sources)
        {
            return !sources.Any();
        }

        public static TSource JoeyFirstOrDefault<TSource>(this IEnumerable<TSource> employees)
        {
            var enumerator = employees.GetEnumerator();

            if (enumerator.MoveNext())
            {
                return enumerator.Current;
            }

            return default(TSource);
        }
    }
}