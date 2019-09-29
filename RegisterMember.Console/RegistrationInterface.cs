using RegisterMember.Models;
using RegisterMember.Service;
using System;
using System.Globalization;
using Serilog;

namespace RegisterMember.App
{
    public class RegistrationInterface
    {
        private readonly Registration _registration;
        public RegistrationInterface(Registration registration)
        {
            _registration = registration;
        }

        public Member GetMemberInformation()
        {
            try
            {
                Person person = GetPersonInformation();
                CheckAgeRequirements(person);

                Member member = _registration.MapPersonToMember(person);

                member.MaritalStatus = GetMaritalStatus();
                member.Spouse = null;
                if (member.MaritalStatus == "married")
                {
                    Console.WriteLine("Please enter your spouse's details >>");
                    member.Spouse = GetPersonInformation();
                }
                return member;
            }
            catch(Exception ex)
            {
                Log.Error(ex.Message);
                return null;
            }
        }

        public Person GetPersonInformation()
        {
            Person person = new Person();
            person.Id = Guid.NewGuid();
            person.FirstName = GetTextInput("Please enter first name: ");
            person.Surname = GetTextInput("Please enter surname: ");
            person.DateOfBirth = GetDateInput("Please enter date of birth (e.g. 30/01/2001): ");

            return person;
        }

        public string GetTextInput(string text)
        {
            Console.Write(text);
            return Console.ReadLine();
        }

        public DateTime GetDateInput(string text)
        {
            string inputdate = GetTextInput(text);
            if (_registration.IsValidDate(inputdate))
            {
                return DateTime.ParseExact(inputdate, "dd/MM/yyyy", DateTimeFormatInfo.InvariantInfo); ;
            }
            else
            {
                Console.WriteLine("The date you entered is invalid.");
            }

            return GetDateInput(text);
        }

        public void CheckAgeRequirements(Person person)
        {
            if (_registration.IsBelowAge(person))
            {
                throw new Exception("I'm sorry, you are not old enough to register.");
            }
            if (_registration.IsRequireParentalAuthorization(person))
            {
                if (GetParentalConsent() == false)
                {
                    throw new Exception("I'm sorry, parental authorization is required.");
                }
            }
        }

        public bool GetParentalConsent()
        {
            string input = GetTextInput("Does your parents allow you to register?\n Yes (Y), No (N): ").ToLowerInvariant();
            if (_registration.IsValidYesNo(input))
            {
                if (input == "n" || input == "no")
                {
                    return false;
                }
                return true;
            }
            else
            {
                return GetParentalConsent();
            }
        }

        public string GetMaritalStatus()
        {
            string input = GetTextInput("Please enter marital status \nMarried (M), Single (S), Divorced (D), Widowed(W): ").ToLowerInvariant();
            if (_registration.IsValidMaritalStatus(input))
            {
                return _registration.FormatMaritalStatusInput(input);
            }
            else
            {
                return GetMaritalStatus();
            }
        }
    }
}
