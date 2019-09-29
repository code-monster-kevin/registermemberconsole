using NUnit.Framework;
using RegisterMember.Models;
using RegisterMember.Service;
using System;

namespace RegisterMember.NUnit.Test
{
    class RegisterServiceTest
    {
        private Registration _registration;

        [SetUp]
        public void Setup()
        {
            _registration = Registration.Instance;
        }

        [TestCase("40/40/4444")]
        [TestCase("12/31/2020")]
        [TestCase("31/02/2020")]
        public void TestInValidDates(string inputdate)
        {
            Assert.IsFalse(_registration.IsValidDate(inputdate));
        }

        [TestCase("23/11/2019")]
        [TestCase("01/12/1988")]
        [TestCase("31/07/1998")]
        public void TestValidDates(string inputdate)
        {
            Assert.IsTrue(_registration.IsValidDate(inputdate));
        }

        [TestCase(13)]
        [TestCase(14)]
        [TestCase(15)]
        public void TestUnderAge(int age)
        {
            DateTime dateOfBirth = DateTime.Now.AddYears(-age);
            Person person = new Person
            {
                Id = Guid.NewGuid(),
                FirstName = "TestUnderAge",
                Surname = "Tester",
                DateOfBirth = dateOfBirth
            };

            Assert.IsTrue(_registration.IsBelowAge(person));
        }

        [TestCase(21)]
        [TestCase(31)]
        [TestCase(41)]
        public void TestEligibleAge(int age)
        {
            DateTime dateOfBirth = DateTime.Now.AddYears(-age);
            Person person = new Person
            {
                Id = Guid.NewGuid(),
                FirstName = "TestUnderAge",
                Surname = "Tester",
                DateOfBirth = dateOfBirth
            };

            Assert.IsFalse(_registration.IsBelowAge(person));
        }

        [TestCase(18)]
        public void TestIsRequireParentalAuthorization(int age)
        {
            DateTime dateOfBirth = DateTime.Now.AddYears(-age);
            Person person = new Person
            {
                Id = Guid.NewGuid(),
                FirstName = "TestUnderAge",
                Surname = "Tester",
                DateOfBirth = dateOfBirth
            };

            Assert.IsTrue(_registration.IsRequireParentalAuthorization(person));
        }

        [TestCase(21)]
        public void TestNotRequireParentalAuthorization(int age)
        {
            DateTime dateOfBirth = DateTime.Now.AddYears(-age);
            Person person = new Person
            {
                Id = Guid.NewGuid(),
                FirstName = "TestUnderAge",
                Surname = "Tester",
                DateOfBirth = dateOfBirth
            };

            Assert.IsFalse(_registration.IsRequireParentalAuthorization(person));
        }

        [TestCase("yes")]
        [TestCase("y")]
        [TestCase("no")]
        [TestCase("n")]
        public void TestIsValidYesNo(string input)
        {
            Assert.IsTrue(_registration.IsValidYesNo(input));
        }

        [TestCase("")]
        [TestCase("xyz")]
        [TestCase("abc")]
        public void TestInValidYesNo(string input)
        {
            Assert.IsFalse(_registration.IsValidYesNo(input));
        }

        [TestCase("married")]
        [TestCase("m")]
        [TestCase("single")]
        [TestCase("s")]
        [TestCase("divorced")]
        [TestCase("d")]
        [TestCase("widowed")]
        [TestCase("w")]
        public void TestIsValidMaritalStatus(string input)
        {
            Assert.IsTrue(_registration.IsValidMaritalStatus(input));
        }

        [TestCase("m")]
        [TestCase("s")]
        [TestCase("d")]
        [TestCase("w")]
        public void TestFormatMaritalStatus(string input)
        {
            switch(input)
            {
                case "m":
                    Assert.AreEqual("married", _registration.FormatMaritalStatusInput(input));
                    break;
                case "s":
                    Assert.AreEqual("single", _registration.FormatMaritalStatusInput(input));
                    break;
                case "d":
                    Assert.AreEqual("divorced", _registration.FormatMaritalStatusInput(input));
                    break;
                case "w":
                    Assert.AreEqual("widowed", _registration.FormatMaritalStatusInput(input));
                    break;
                default:
                    break;
            }
        }

    }
}
