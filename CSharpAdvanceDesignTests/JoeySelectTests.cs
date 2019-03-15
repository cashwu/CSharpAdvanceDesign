using System;
using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Lab;

namespace CSharpAdvanceDesignTests
{
    [TestFixture()]
    public class JoeySelectTests
    {
        [Test]
        public void replace_http_to_https()
        {
            var urls = GetUrls();

            var actual = urls.JoeySelect(url => url.Replace("http://", "https://"));
            var expected = new List<string>
            {
                "https://tw.yahoo.com",
                "https://facebook.com",
                "https://twitter.com",
                "https://github.com",
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [Test]
        public void replace_http_to_https_and_append_joey()
        {
            var urls = GetUrls();

            var actual = urls.JoeySelect(url => url.Replace("http://", "https://") + "/joey");
            var expected = new List<string>
            {
                "https://tw.yahoo.com/joey",
                "https://facebook.com/joey",
                "https://twitter.com/joey",
                "https://github.com/joey"
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [Test]
        public void get_full_name()
        {
            var employees = GetEmployees();

            var actual = employees.JoeySelect(employee => $"{employee.FirstName}-{employee.LastName}");
            var expected = new List<string>
            {
                "Joey-Chen",
                "Tom-Li",
                "David-Chen"
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [Test]
        public void get_full_name_length()
        {
            var employees = GetEmployees();

            var actual = employees.JoeySelect(employee => $"{employee.FirstName}{employee.LastName}".Length);
            var expected = new List<int>
            {
                8,
                5,
                9
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [Test]
        public void get_full_name_with_seq_no()
        {
            var employees = GetEmployees();

            var actual = employees.JoeySelect((employee, index) => $"{index + 1}.{employee.FirstName}-{employee.LastName}");
            var expected = new List<string>
            {
                "1.Joey-Chen",
                "2.Tom-Li",
                "3.David-Chen"
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        private static IEnumerable<string> GetUrls()
        {
            yield return "http://tw.yahoo.com";
            yield return "https://facebook.com";
            yield return "https://twitter.com";
            yield return "http://github.com";
        }

        private static List<Employee> GetEmployees()
        {
            return new List<Employee>
            {
                new Employee { FirstName = "Joey", LastName = "Chen" },
                new Employee { FirstName = "Tom", LastName = "Li" },
                new Employee { FirstName = "David", LastName = "Chen" }
            };
        }
    }
}