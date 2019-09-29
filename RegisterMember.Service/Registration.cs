using RegisterMember.Data;
using RegisterMember.Models;
using System;
using System.Globalization;

namespace RegisterMember.Service
{
    public sealed class Registration
    {
        const int underAge = 16;
        const int parentalAuthorizationAge = 18;

        private static readonly Lazy<Registration> lazy = new Lazy<Registration>(() => new Registration());

        public static Registration Instance { get { return lazy.Value; } }

        private Registration() { }

        public bool RegisterMember(Member member)
        {
            return MemberData.Save(member);
        }

        public bool IsValidDate(string input)
        {
            try
            {
                DateTime.ParseExact(input, "dd/MM/yyyy", DateTimeFormatInfo.InvariantInfo);
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public bool IsBelowAge(Person person)
        {
            if (person.GetAge() < underAge)
            {
                return true;
            }
            return false;
        }

        public bool IsRequireParentalAuthorization(Person person)
        {
            if (person.GetAge() <= parentalAuthorizationAge)
            {
                return true;
            }
            return false;
        }

        public bool IsValidYesNo(string input)
        {
            string[] validValues = { "yes", "y", "no", "n" };
            return CompareUtility.checkValidInput(validValues, input);
        }

        public bool IsValidMaritalStatus(string input)
        {
            string[] validValues = { "married", "m", "single", "s", "divorced", "d", "widowed", "w" };
            return CompareUtility.checkValidInput(validValues, input);
        }

        public string FormatMaritalStatusInput(string input)
        {
            string formattedinput = input.ToLowerInvariant();
            switch (input.ToLowerInvariant())
            {
                case "m":
                    formattedinput = "married";
                    break;
                case "s":
                    formattedinput = "single";
                    break;
                case "d":
                    formattedinput = "divorced";
                    break;
                case "w":
                    formattedinput = "widowed";
                    break;
                default:
                    break;
            }
            return formattedinput;
        }

        public Member MapPersonToMember(Person person)
        {
            return new Member
            {
                Id = person.Id,
                FirstName = person.FirstName,
                Surname = person.Surname,
                DateOfBirth = person.DateOfBirth
            };
        }
    }
}
