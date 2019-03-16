using System;
using System.Collections;
using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CSharpAdvanceDesignTests
{
    public class CombineKeyComparer<TKey> : IComparer<Employee>
    {
        public CombineKeyComparer(Func<Employee, TKey> keySelector, IComparer keyComparer)
        {
            KeySelector = keySelector;
            KeyComparer = keyComparer;
        }

        private Func<Employee, TKey> KeySelector { get; }
        private IComparer KeyComparer { get; }

        public int Compare(Employee element, Employee minElement)
        {
            return KeyComparer.Compare(KeySelector(element), KeySelector(minElement));
        }
    }

    public class ComboComparer : IComparer<Employee>
    {
        public ComboComparer(IComparer<Employee> firstComparer, IComparer<Employee> secondComparer)
        {
            FirstComparer = firstComparer;
            SecondComparer = secondComparer;
        }

        public IComparer<Employee> FirstComparer { get; private set; }
        public IComparer<Employee> SecondComparer { get; private set; }

        public int Compare(Employee x, Employee y)
        {
            var firstCompareResult = FirstComparer.Compare(x, y);
            if (firstCompareResult == 0)
            {
                return SecondComparer.Compare(x, y);
            }

            return firstCompareResult;
        }
    }

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

            var firstComparer = new CombineKeyComparer<string>(element => element.LastName, Comparer<string>.Default);
            var secondComparer = new CombineKeyComparer<string>(element => element.FirstName, Comparer<string>.Default);
            var actual = JoeyOrderBy(employees, new ComboComparer(firstComparer, secondComparer));

            var expected = new[]
            {
                new Employee { FirstName = "Joey", LastName = "Chen" },
                new Employee { FirstName = "Joseph", LastName = "Chen" },
                new Employee { FirstName = "Tom", LastName = "Li" },
                new Employee { FirstName = "Joey", LastName = "Wang" }
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        [Test]
        public void orderBy_lastName_and_firstName_and_age()
        {
            var employees = new[]
            {
                new Employee { FirstName = "Joey", LastName = "Wang", Age = 50 },
                new Employee { FirstName = "Tom", LastName = "Li", Age = 31 },
                new Employee { FirstName = "Joseph", LastName = "Chen", Age = 32 },
                new Employee { FirstName = "Joey", LastName = "Chen", Age = 33 },
                new Employee { FirstName = "Joey", LastName = "Wang", Age = 20 }
            };

            var firstComparer = new CombineKeyComparer<string>(element => element.LastName, Comparer<string>.Default);
            var secondComparer = new CombineKeyComparer<string>(element => element.FirstName, Comparer<string>.Default);

            var firstCombo = new ComboComparer(firstComparer, secondComparer);

            var thirdComparer = new CombineKeyComparer<int>(element => element.Age, Comparer<int>.Default);

            var finalCombo = new ComboComparer(firstCombo, thirdComparer);

            var actual = JoeyOrderBy(employees, finalCombo);

            var expected = new[]
            {
                new Employee { FirstName = "Joey", LastName = "Chen", Age = 33 },
                new Employee { FirstName = "Joseph", LastName = "Chen", Age = 32 },
                new Employee { FirstName = "Tom", LastName = "Li", Age = 31 },
                new Employee { FirstName = "Joey", LastName = "Wang", Age = 20 },
                new Employee { FirstName = "Joey", LastName = "Wang", Age = 50 },
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<Employee> JoeyOrderBy(IEnumerable<Employee> employees,
                                                  IComparer<Employee> comboComparer)
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

                    if (comboComparer.Compare(element, minElement) < 0)
                    {
                        minElement = element;
                        index = i;
                    }
                }

                elements.RemoveAt(index);
                yield return minElement;
            }
        }
    }
}