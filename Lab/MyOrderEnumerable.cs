using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Lab.Entities;

namespace Lab
{
    public class MyOrderEnumerable : IOrderedEnumerable<Employee>
    {
        private readonly IEnumerable<Employee> _sources;
        private readonly IComparer<Employee> _untilComparer;

        public MyOrderEnumerable(IEnumerable<Employee> sources, IComparer<Employee> untilComparer)
        {
            _sources = sources;
            _untilComparer = untilComparer;
        }

        public IOrderedEnumerable<Employee> CreateOrderedEnumerable<TKey>(Func<Employee, TKey> keySelector,
                                                                          IComparer<TKey> comparer,
                                                                          bool @descending)
        {
            return new MyOrderEnumerable(_sources, new ComboComparer(_untilComparer, new CombineKeyComparer<TKey>(keySelector, comparer)));
        }

        public IEnumerator<Employee> GetEnumerator()
        {
            //bubble sort
            var elements = _sources.ToList();
            while (elements.Any())
            {
                var minElement = elements[0];
                var index = 0;
                for (int i = 1; i < elements.Count; i++)
                {
                    var element = elements[i];

                    if (_untilComparer.Compare(element, minElement) < 0)
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