﻿using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Collections.Generic;

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

            var actual = JoeyTake(employees);

            var expected = new List<Employee>
            {
                new Employee { FirstName = "Joey", LastName = "Chen" },
                new Employee { FirstName = "Tom", LastName = "Li" },
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<Employee> JoeyTake(IEnumerable<Employee> employees)
        {
            var enumerator = employees.GetEnumerator();

            var index = 0;
            while (enumerator.MoveNext())
            {
                yield return enumerator.Current;
                index++;
                
                if (index == 2)
                {
                    yield break;
                }
            }
        }
    }
}