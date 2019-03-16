using Lab.Entities;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeyFirstOrDefaultTests
    {
        [Test]
        public void get_null_when_employees_is_empty()
        {
            var employees = new List<Employee>();

            var actual = JoeyFirstOrDefault(employees);

            Assert.IsNull(actual);
        }

        [Test]
        public void numbers_is_empty()
        {
            var items = new List<int>();

            var actual = JoeyFirstOrDefault(items);

            Assert.AreEqual(0, actual);
        }

        private TSource JoeyFirstOrDefault<TSource>(IEnumerable<TSource> employees)
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