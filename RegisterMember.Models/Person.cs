using System;
using System.Collections.Generic;
using System.Text;

namespace RegisterMember.Models
{
    public class Person
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }

        public int GetAge()
        {
            int age = 0;
            age = DateTime.Now.Year - DateOfBirth.Year;
            if (DateTime.Now.DayOfYear < DateOfBirth.DayOfYear) age = age - 1;
            return age;
        }
    }
}
