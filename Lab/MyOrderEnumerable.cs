using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Lab.Entities;

namespace Lab
{
    public class MyOrderEnumerable : IOrderedEnumerable<Employee>
    {
        private readonly IEnumerable<Employee> _source;
        private readonly IComparer<Employee> _comparer;

        public MyOrderEnumerable(IEnumerable<Employee> source, IComparer<Employee> comparer)
        {
            _source = source;
            _comparer = comparer;
        }

        public IOrderedEnumerable<Employee> CreateOrderedEnumerable<TKey>(Func<Employee, TKey> keySelector,
                                                                          IComparer<TKey> lastComparer,
                                                                          bool @descending)
        {
            var combineKeyComparer = new CombineKeyComparer<TKey>(keySelector, lastComparer);
            return new MyOrderEnumerable(_source, new ComboComparer(_comparer, combineKeyComparer));
        }

        public IEnumerator<Employee> GetEnumerator()
        {
            var elements = _source.ToList();
            while (elements.Any())
            {
                var minElement = elements[0];
                var index = 0;
                for (int i = 1; i < elements.Count; i++)
                {
                    var element = elements[i];

                    if (_comparer.Compare(element, minElement) < 0)
                    {
                        minElement = element;
                        index = i;
                    }
                }

                elements.RemoveAt(index);
                yield return minElement;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}