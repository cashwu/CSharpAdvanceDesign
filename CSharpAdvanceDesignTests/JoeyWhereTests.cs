using Lab.Entities;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using ExpectedObjects;
using Lab;

namespace CSharpAdvanceDesignTests
{
    [TestFixture()]
    public class JoeyWhereTests
    {
        [Test]
        public void group_sum_group_count_is_3_sum_cost()
        {
            var products = new List<Product>
            {
                new Product { Id = 1, Cost = 11, Price = 110, Supplier = "Odd-e" },
                new Product { Id = 2, Cost = 21, Price = 210, Supplier = "Yahoo" },
                new Product { Id = 3, Cost = 31, Price = 310, Supplier = "Odd-e" },
                new Product { Id = 4, Cost = 41, Price = 410, Supplier = "Odd-e" },
                new Product { Id = 5, Cost = 51, Price = 510, Supplier = "Momo" },
                new Product { Id = 6, Cost = 61, Price = 610, Supplier = "Momo" },
                new Product { Id = 7, Cost = 71, Price = 710, Supplier = "Yahoo" },
                new Product { Id = 8, Cost = 18, Price = 780, Supplier = "Yahoo" }
            };

            var expected = new[]
            {
                63, 153, 89
            };

            var actual = JoeyGroupSum(products);
            
            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<int> JoeyGroupSum(List<Product> products)
        {
            // var groupCount = products.Count / 3;

            for (var i = 0; i <= products.Count; i += 3)
            {
                yield return products.Skip(i).Take(3).Sum(a => a.Cost);
            }
            
            // yield return products.Skip(0).Take(3).Sum(a => a.Cost);
            // yield return products.Skip(1 * 3).Take(3).Sum(a => a.Cost);
            // yield return products.Skip(2 * 3).Take(3).Sum(a => a.Cost);
        }

        [Test]
        public void find_products_that_price_between_200_and_500()
        {
            var products = new List<Product>
            {
                new Product { Id = 1, Cost = 11, Price = 110, Supplier = "Odd-e" },
                new Product { Id = 2, Cost = 21, Price = 210, Supplier = "Yahoo" },
                new Product { Id = 3, Cost = 31, Price = 310, Supplier = "Odd-e" },
                new Product { Id = 4, Cost = 41, Price = 410, Supplier = "Odd-e" },
                new Product { Id = 5, Cost = 51, Price = 510, Supplier = "Momo" },
                new Product { Id = 6, Cost = 61, Price = 610, Supplier = "Momo" },
                new Product { Id = 7, Cost = 71, Price = 710, Supplier = "Yahoo" },
                new Product { Id = 8, Cost = 18, Price = 780, Supplier = "Yahoo" }
            };

            var actual = products.JoeyWhere(product => product.Price > 200 && product.Price < 500);

            var expected = new List<Product>
            {
                new Product { Id = 2, Cost = 21, Price = 210, Supplier = "Yahoo" },
                new Product { Id = 3, Cost = 31, Price = 310, Supplier = "Odd-e" },
                new Product { Id = 4, Cost = 41, Price = 410, Supplier = "Odd-e" }
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        [Test]
        public void where_select()
        {
            var products = new List<Product>
            {
                new Product { Id = 1, Cost = 11, Price = 110, Supplier = "Odd-e" },
                new Product { Id = 2, Cost = 21, Price = 210, Supplier = "Yahoo" },
                new Product { Id = 3, Cost = 31, Price = 310, Supplier = "Odd-e" },
                new Product { Id = 4, Cost = 41, Price = 410, Supplier = "Odd-e" },
                new Product { Id = 5, Cost = 51, Price = 510, Supplier = "Momo" },
                new Product { Id = 6, Cost = 61, Price = 610, Supplier = "Momo" },
                new Product { Id = 7, Cost = 71, Price = 710, Supplier = "Yahoo" },
                new Product { Id = 8, Cost = 18, Price = 780, Supplier = "Yahoo" }
            };

            var actual = products.JoeyWhere(product => product.Price > 200 && product.Price < 500)
                                 .JoeySelect(p => p.Price)
                                 .ToList();

            var expected = new[]
            {
                210,
                310,
                410
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        [Test]
        public void find_name_short()
        {
            var str = new List<string> { "aa", "bbb", "cccc" };

            var actual = str.JoeyWhere(s => s.Length > 3);
            var expected = new[] { "cccc" };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        [Test]
        public void find_odd_name()
        {
            var str = new List<string> { "aa", "bbb", "cccc" };

            var actual = str.JoeyWhere((s, i) => i % 2 == 0);
            var expected = new[] { "aa", "cccc" };

            expected.ToExpectedObject().ShouldMatch(actual);
        }
    }
}