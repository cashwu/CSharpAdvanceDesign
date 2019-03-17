using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeyAggregateTests
    {
        [Test]
        public void drawling_money_that_balance_have_to_be_positive()
        {
            var balance = 100.91m;

            var drawlingList = new List<int>
            {
                30, 80, 20, 40, 25
            };

            var actual = JoeyAggregate(drawlingList,
                                       balance, (current, seed) =>
                                       {
                                           if (current <= seed)
                                           {
                                               seed -= current;
                                           }

                                           return seed;
                                       }, seed1 => seed1.ToString());

            var expected = "10.91";

            Assert.AreEqual(expected, actual);
        }

        private string JoeyAggregate(IEnumerable<int> drawlingList, 
                                     decimal balance,
                                     Func<int, decimal, decimal> func,
                                     Func<decimal, string> resultSelector)
        {
            var enumerator = drawlingList.GetEnumerator();

            decimal seed = balance;
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;

                seed = func(current, seed);
            }
            
            return resultSelector(seed);
        }
    }
}