using System;
using System.Collections.Generic;
using System.Text;

namespace RegisterMember.Models
{
    public class Member : Person
    {
        public string MaritalStatus { get; set; }
        public Person Spouse { get; set; }
    }
}
