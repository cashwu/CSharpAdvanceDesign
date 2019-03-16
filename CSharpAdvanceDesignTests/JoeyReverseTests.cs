using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using System.Collections.Generic;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeyReverseTests
    {
        [Test]
        public void reverse_employees()
        {
            var employees = new List<Employee>
            {
                new Employee(){FirstName = "Joey",LastName = "Chen"},
                new Employee(){FirstName = "Tom",LastName = "Li"},
                new Employee(){FirstName = "David",LastName = "Wang"},
            };

            var actual = JoeyReverse(employees);

            var expected = new List<Employee>
            {
                new Employee(){FirstName = "David",LastName = "Wang"},
                new Employee(){FirstName = "Tom",LastName = "Li"},
                new Employee(){FirstName = "Joey",LastName = "Chen"},
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<TSource> JoeyReverse<TSource>(IEnumerable<TSource> employees)
        {
            return new Stack<TSource>(employees);
            
            // var enumerator = employees.GetEnumerator();
            //
            // var s = new Stack<Employee>();
            // while (enumerator.MoveNext())
            // {
            //     s.Push(enumerator.Current);
            // }
            //
            // while (s.Count > 0)
            // {
            //     yield return (TSource)s.Pop();
            // }
        }
    }
}