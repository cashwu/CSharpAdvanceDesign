using System;
using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeyOrderByTests
    {
        [Test]
        [Ignore("ignore")]
        public void orderBy_lastName()
        {
            // var employees = new[]
            // {
            //     new Employee { FirstName = "Joey", LastName = "Wang" },
            //     new Employee { FirstName = "Tom", LastName = "Li" },
            //     new Employee { FirstName = "Joseph", LastName = "Chen" },
            //     new Employee { FirstName = "Joey", LastName = "Chen" },
            // };
            //
            // var actual = JoeyOrderByLastName(employees, element => element.LastName);
            //
            // var expected = new[]
            // {
            //     new Employee { FirstName = "Joseph", LastName = "Chen" },
            //     new Employee { FirstName = "Joey", LastName = "Chen" },
            //     new Employee { FirstName = "Tom", LastName = "Li" },
            //     new Employee { FirstName = "Joey", LastName = "Wang" },
            // };
            //
            // expected.ToExpectedObject().ShouldMatch(actual);
        }

        [Test]
        public void orderBy_lastName_and_firstName()
        {
            var employees = new[]
            {
                new Employee { FirstName = "Joey", LastName = "Wang" },
                new Employee { FirstName = "Tom", LastName = "Li" },
                new Employee { FirstName = "Joseph", LastName = "Chen" },
                new Employee { FirstName = "Joey", LastName = "Chen" }
            };

            var actual = JoeyOrderByLastNameAndFirstName(employees,
                                                         element => element.LastName,
                                                         Comparer<string>.Default, 
                                                         element => element.FirstName,
                                                         Comparer<string>.Default);

            var expected = new[]
            {
                new Employee { FirstName = "Joey", LastName = "Chen" },
                new Employee { FirstName = "Joseph", LastName = "Chen" },
                new Employee { FirstName = "Tom", LastName = "Li" },
                new Employee { FirstName = "Joey", LastName = "Wang" }
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<Employee> JoeyOrderByLastNameAndFirstName(IEnumerable<Employee> employees,
                                                                      Func<Employee, string> keySelector,
                                                                      Comparer<string> firstKeyComparer,
                                                                      Func<Employee, string> secondKeySelector,
                                                                      Comparer<string> secondKeyComparer)
        {
            //bubble sort

            var elements = employees.ToList();
            while (elements.Any())
            {
                var minElement = elements[0];
                var index = 0;
                for (int i = 1; i < elements.Count; i++)
                {
                    var element = elements[i];
                    var firstCompareResult = firstKeyComparer.Compare(keySelector(element), keySelector(minElement));
                    if (firstCompareResult < 0)
                    {
                        minElement = element;
                        index = i;
                    }
                    else if (firstCompareResult == 0)
                    {
                        if (secondKeyComparer.Compare(secondKeySelector(element), secondKeySelector(minElement)) < 0)
                        {
                            minElement = element;
                            index = i;
                        }
                    }
                }

                elements.RemoveAt(index);
                yield return minElement;
            }
        }
    }
}