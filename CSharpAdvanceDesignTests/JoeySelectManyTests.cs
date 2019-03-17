using System;
using ExpectedObjects;
using NUnit.Framework;
using System.Collections.Generic;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeySelectManyTests
    {
        [Test]
        public void flat_all_cities_and_sections()
        {
            var cities = new List<City>
            {
                new City { Name = "台北市", Sections = new List<string> { "大同", "大安", "松山" } },
                new City { Name = "新北市", Sections = new List<string> { "三重", "新莊" } },
            };

            var actual = JoeySelectMany(cities,
                                        cityCurrent => cityCurrent.Sections,
                                        (city, section) => $"{city.Name}-{section}");

            var expected = new[]
            {
                "台北市-大同",
                "台北市-大安",
                "台北市-松山",
                "新北市-三重",
                "新北市-新莊",
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<TResult> JoeySelectMany<TSource, TCollection, TResult>(IEnumerable<TSource> sources,
                                                   Func<TSource, IEnumerable<TCollection>> collectionSelector,
                                                   Func<TSource, TCollection, TResult> resultSelector)
        {
            var enumerator = sources.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var cityCurrent = enumerator.Current;
                var sectionEnumerator = collectionSelector(cityCurrent).GetEnumerator();

                while (sectionEnumerator.MoveNext())
                {
                    var sectionCurrent = sectionEnumerator.Current;
                    yield return resultSelector(cityCurrent, sectionCurrent);
                }
            }
        }
    }

    public class City
    {
        public string Name { get; set; }
        public IEnumerable<string> Sections { get; set; }
    }
}