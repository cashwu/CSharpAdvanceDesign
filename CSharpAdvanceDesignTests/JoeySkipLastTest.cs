using System.Collections.Generic;
using System.Linq;
using ExpectedObjects;
using Lab;
using Lab.Entities;
using NUnit.Framework;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeySkipLastTest
    {
        [Test]
        public void skip_last_2()
        {
            var numbers = new List<int> { 10, 20, 30, 40, 50, 60 }; // 6

            var actual = JoeySkipLast(numbers, 2);

            var expected = new[] { 10, 20, 30, 40 };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<int> JoeySkipLast(IEnumerable<int> numbers, int count)
        {
            // var numberEnumerator = numbers.GetEnumerator();
            // var index = 0;
            // while (numberEnumerator.MoveNext())
            // {
            //     index++;
            // } 
            //
            // numberEnumerator.Reset();
            //
            // index = index - count;
            // while (numberEnumerator.MoveNext())
            // {
            //     index--;
            //     if (index >= 0)
            //     {
            //         yield return numberEnumerator.Current;
            //     }
            // } 
            
            // ------ 2 --------
            // var queue = new Queue<int>(numbers);
            // var queueCount = queue.Count;
            // for (int i = 0; i < queueCount - count; i++)
            // {
            //     yield return queue.Dequeue();
            // }
            
            
            // ------ 3 --------
            
            var queue = new Queue<int>();
            var enumerator = numbers.GetEnumerator();

            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;

                if (queue.Count == count)
                {
                    yield return queue.Dequeue();
                }
                
                queue.Enqueue(current);
            }
        }
    }
}