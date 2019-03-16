using System;
using System.Collections.Generic;

namespace Lab.Entities
{
    public class JoeyEmployeeEqualityComparer : IEqualityComparer<Employee>
    {
        public bool Equals(Employee x, Employee y)
        {
            return x.FirstName == y.FirstName
                   && x.LastName == y.LastName;
        }

        public int GetHashCode(Employee obj)
        {
            var firstNameCode = obj.FirstName;
            var LastNameCode = obj.LastName;

            return Tuple.Create(firstNameCode, LastNameCode).GetHashCode();
            // return new { firstNameCode, LastNameCode }.GetHashCode();
        }
    }

    public class JoeyEmployeeWihtPhoneEqualityComparer : IEqualityComparer<Employee>
    {
        public bool Equals(Employee x, Employee y)
        {
            return x.FirstName == y.FirstName
                   && x.LastName == y.LastName
                   && x.Phone == y.Phone;
        }

        public int GetHashCode(Employee obj)
        {
            throw new System.NotImplementedException();
        }
    }

    public class Employee
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public Role Role { get; set; }

        public string Phone { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Employee e)
            {
                if (FirstName == e.FirstName && LastName == e.LastName)
                {
                    return true;
                }
            }

            return false;
        }
    }
}