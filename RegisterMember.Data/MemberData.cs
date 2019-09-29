using RegisterMember.Models;
using System;

namespace RegisterMember.Data
{
    public static class MemberData
    {
        const string peopleFilePath = "c:\\sandbox\\people.txt";
        const string spouseFilePath = "c:\\sandbox\\spouse.txt";

        public static bool Save(Member member)
        {
            try
            {
                string memberContents = ConvertToString(member);
                FileService.WriteFile(peopleFilePath, memberContents);
                if (member.Spouse != null)
                {
                    string spouseContents = ConvertToString(member.Spouse);
                    FileService.WriteFile(spouseFilePath, spouseContents);
                }
                return true;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static string ConvertToString(Member member)
        {
            string SpouseId = "null";
            if (member.Spouse != null)
            {
                SpouseId = member.Spouse.Id.ToString();
            }

            return String.Format("{0}|{1}|{2}|{3}|{4}|{5}",
                                 member.Id,
                                 member.FirstName,
                                 member.Surname,
                                 member.DateOfBirth.ToString("dd-MM-yyyy"),
                                 member.MaritalStatus,
                                 SpouseId);
        }

        private static string ConvertToString(Person person)
        {
            return String.Format("{0}|{1}|{2}|{3}",
                                 person.Id,
                                 person.FirstName,
                                 person.Surname,
                                 person.DateOfBirth.ToString("dd-MM-yyyy"));
        }
    }
}
