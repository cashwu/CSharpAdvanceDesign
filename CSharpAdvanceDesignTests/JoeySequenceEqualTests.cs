using NUnit.Framework;
using System.Collections.Generic;
using Lab.Entities;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeySequenceEqualTests
    {
        [Test]
        public void compare_two_numbers_equal()
        {
            var first = new List<int> { 3, 2, 1 };
            var second = new List<int> { 3, 2, 1 };

            var actual = JoeySequenceEqual(first, second);

            Assert.IsTrue(actual);
        }

        [Test]
        public void compare_two_numbers_not_equal()
        {
            var first = new List<int> { 3, 2, 1 };
            var second = new List<int> { 1, 2, 3 };

            var actual = JoeySequenceEqual(first, second);

            Assert.IsFalse(actual);
        }

        [Test]
        public void compare_two_numbers_first_length_more_than_second_not_equal()
        {
            var first = new List<int> { 3, 2 };
            var second = new List<int> { 3, 2, 1 };

            var actual = JoeySequenceEqual(first, second);

            Assert.IsFalse(actual);
        }

        [Test]
        public void compare_two_numbers_second_length_more_than_first_not_equal()
        {
            var first = new List<int> { 3, 2, 1 };
            var second = new List<int> { 3, 2 };

            var actual = JoeySequenceEqual(first, second);

            Assert.IsFalse(actual);
        }

        [Test]
        public void empty()
        {
            var first = new List<int>();
            var second = new List<int>();

            var actual = JoeySequenceEqual(first, second);

            Assert.IsTrue(actual);
        }

        [Test]
        public void two_employees_sequence_equal()
        {
            var first = new List<Employee>
            {
                new Employee() { FirstName = "Joey", LastName = "Chen" },
                new Employee() { FirstName = "Tom", LastName = "Li" },
                new Employee() { FirstName = "David", LastName = "Wang" },
            };
            var second = new List<Employee>
            {
                new Employee() { FirstName = "Joey", LastName = "Chen" },
                new Employee() { FirstName = "Tom", LastName = "Li" },
                new Employee() { FirstName = "David", LastName = "Wang" },
            };

            var actual = JoeySequenceEqual(first, second, new JoeyEmployeeEqualityComparer());

            Assert.IsTrue(actual);
        }

        private bool JoeySequenceEqual<TSource>(IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> equalityComparer)
        {
            var firstEnumerator = first.GetEnumerator();
            var secondEnumerator = second.GetEnumerator();

            while (true)
            {
                var firstFlag = firstEnumerator.MoveNext();
                var secondFlag = secondEnumerator.MoveNext();

                if (IsLengthDifferent(firstFlag, secondFlag))
                {
                    return false;
                }
                
                if (IsEnd(firstFlag))
                {
                    return true;
                }

                var firstCurrent = firstEnumerator.Current;
                var secondCurrent = secondEnumerator.Current;
                if (!equalityComparer.Equals(firstCurrent, secondCurrent))
                {
                    return false;
                }
            }
        }

        private bool JoeySequenceEqual<TSource>(IEnumerable<TSource> first, IEnumerable<TSource> second)
        {
            return JoeySequenceEqual(first, second, EqualityComparer<TSource>.Default);
        }

        private static bool IsEnd(bool firstFlag)
        {
            return !firstFlag;
        }

        private static bool IsLengthDifferent(bool firstFlag, bool secondFlag)
        {
            return firstFlag != secondFlag;
        }
    }
}