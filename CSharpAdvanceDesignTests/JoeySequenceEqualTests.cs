using NUnit.Framework;
using System.Collections.Generic;

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
        
       

        private bool JoeySequenceEqual(IEnumerable<int> first, IEnumerable<int> second)
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

                if (IsValueDifferent(firstEnumerator, secondEnumerator))
                {
                    return false;
                }

                if (IsEnd(firstFlag))
                {
                    return true;
                }
            }

            
            // var isFirstMoveNext = true;
            // var isSecondMoveNext = true;
            // while ((isFirstMoveNext = firstEnumerator.MoveNext()) & (isSecondMoveNext = secondEnumerator.MoveNext()))
            // {
            //     var firstItem = firstEnumerator.Current;
            //     var secondItem = secondEnumerator.Current;
            //
            //     if (firstItem != secondItem)
            //     {
            //         return false;
            //     }
            // }
            //
            // return isFirstMoveNext == isSecondMoveNext;
        }
        
        private static bool IsEnd(bool firstFlag)
        {
            return !firstFlag;
        }

        private static bool IsValueDifferent(IEnumerator<int> firstEnumerator, IEnumerator<int> secondEnumerator)
        {
            return firstEnumerator.Current != secondEnumerator.Current;
        }

        private static bool IsLengthDifferent(bool firstFlag, bool secondFlag)
        {
            return firstFlag != secondFlag;
        }	        
    }
}