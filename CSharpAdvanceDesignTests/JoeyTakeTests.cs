using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using System.Collections.Generic;
using Lab;

namespace CSharpAdvanceDesignTests
{
    [TestFixture()]
    public class JoeyTakeTests
    {
        [Test]
        public void take_2_employees()
        {
            var employees = (IEnumerable<Employee>)new List<Employee>
            {
                new Employee { FirstName = "Joey", LastName = "Chen" },
                new Employee { FirstName = "Tom", LastName = "Li" },
                new Employee { FirstName = "David", LastName = "Chen" },
                new Employee { FirstName = "Mike", LastName = "Chang" },
                new Employee { FirstName = "Joseph", LastName = "Yao" },
            };

            var actual = employees.JoeyTake(2);

            var expected = new List<Employee>
            {
                new Employee { FirstName = "Joey", LastName = "Chen" },
                new Employee { FirstName = "Tom", LastName = "Li" },
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }
    }
}