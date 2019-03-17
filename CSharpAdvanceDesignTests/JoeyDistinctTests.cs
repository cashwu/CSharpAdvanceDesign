using ExpectedObjects;
using NUnit.Framework;
using System.Collections.Generic;
using Lab.Entities;

namespace CSharpAdvanceDesignTests
{
    [TestFixture()]
    public class JoeyDistinctTests
    {
        [Test]
        public void distinct_numbers()
        {
            var numbers = new[] { 91, 3, 91, -1 };
            var actual = JoeyDistinct(numbers);

            var expected = new[] { 91, 3, -1 };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        [Test]
        public void distinct_numbers_2()
        {
            var numbers = new[] { 91, 3, -1, 91, 3 };
            var actual = JoeyDistinct(numbers);

            var expected = new[] { 91, 3, -1 };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        [Test]
        public void distinct_employees()
        {
            var employees = new[]
            {
                new Employee { FirstName = "Joey", LastName = "Chen" },
                new Employee { FirstName = "Joseph", LastName = "Chen" },
                new Employee { FirstName = "Tom", LastName = "Li" },
                new Employee { FirstName = "Joey", LastName = "Chen" },
            };

            var expected = new[]
            {
                new Employee { FirstName = "Joey", LastName = "Chen" },
                new Employee { FirstName = "Joseph", LastName = "Chen" },
                new Employee { FirstName = "Tom", LastName = "Li" },
            };

            var actual = JoeyDistinct(employees, new JoeyEmployeeEqualityComparer());

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<TSource> JoeyDistinct<TSource>(IEnumerable<TSource> sources, IEqualityComparer<TSource> comparer)
        {
            var hashSet = new HashSet<TSource>(comparer);

            var enumerator = sources.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;

                if (hashSet.Add(current))
                {
                    yield return current;
                }
            }
        }

        private IEnumerable<TSource> JoeyDistinct<TSource>(IEnumerable<TSource> sources)
        {
            return JoeyDistinct(sources, EqualityComparer<TSource>.Default);
        }
    }
}