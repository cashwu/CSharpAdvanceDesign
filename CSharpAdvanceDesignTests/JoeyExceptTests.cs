using ExpectedObjects;
using NUnit.Framework;
using System.Collections.Generic;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeyExceptTests
    {
        [Test]
        public void except_numbers_first()
        {
            var first = new[] { 1, 3, 5, 7, 3 }; // 3,5
            var second = new[] { 7, 1, 4, 1 }; // 7,1,4

            var actual = JoeyExcept(first, second);
            var expected = new[] { 3, 5 };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        [Test]
        public void except_numbers_second()
        {
            var first = new[] { 1, 3, 5, 7, 3 };  // 1,3,5,7
            var second = new[] { 7, 1, 4, 1 };  // 4

            var actual = JoeyExcept(second, first);

            var expected = new[] { 4 };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<int> JoeyExcept(IEnumerable<int> first, IEnumerable<int> second)
        {
            var hashSet = new HashSet<int>(second);

            var firstEnumerator = first.GetEnumerator();
            while (firstEnumerator.MoveNext())
            {
                var current = firstEnumerator.Current;

                if (hashSet.Add(current))
                {
                    yield return current;
                }
            }
        }
    }
}