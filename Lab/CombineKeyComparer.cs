using System;
using System.Collections.Generic;
using Lab.Entities;

namespace Lab
{
    public class CombineKeyComparer<TKey> : IComparer<Employee>
    {
        public CombineKeyComparer(Func<Employee, TKey> keySelector, IComparer<TKey> comparer)
        {
            KeySelector = keySelector;
            Comparer = comparer;
        }

        private Func<Employee, TKey> KeySelector { get; set; }
        private IComparer<TKey> Comparer { get; set; }

        public int Compare(Employee element, Employee minElement)
        {
            return Comparer.Compare(KeySelector(element), KeySelector(minElement));
        }
    }
}