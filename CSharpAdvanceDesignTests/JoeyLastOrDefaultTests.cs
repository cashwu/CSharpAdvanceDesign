using Lab.Entities;
using NUnit.Framework;
using System.Collections.Generic;
using ExpectedObjects;
using Lab;

namespace CSharpAdvanceDesignTests
{
    [TestFixture()]
    public class JoeyLastOrDefaultTests
    {
        [Test]
        public void get_null_when_employees_is_empty()
        {
            var employees = new List<Employee>();
            var actual = employees.JoeyLastOrDefault();
            Assert.IsNull(actual);
        }

        [Test]
        public void get_last_employee()
        {
            var employees = new List<Employee>
            {
                new Employee { FirstName = "Joey", LastName = "Chen" },
                new Employee { FirstName = "Cash", LastName = "Wu" },
                new Employee { FirstName = "David", LastName = "Wu" },
            };

            var actual = employees.JoeyLastOrDefault();
            var expected = new Employee { FirstName = "David", LastName = "Wu" };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        [Test]
        public void get_last_number()
        {
            var numbers = new List<int> { 1, 2, 3 };

            var actual = numbers.JoeyLastOrDefault();

            Assert.AreEqual(3, actual);
        }
    }
}