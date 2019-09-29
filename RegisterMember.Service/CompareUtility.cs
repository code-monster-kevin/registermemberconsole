using System;
using System.Collections.Generic;
using System.Text;

namespace RegisterMember.Service
{
    public static class CompareUtility
    {
        public static bool checkValidInput(string[] values, string input)
        {
            foreach (string s in values)
            {
                if (input.ToLowerInvariant() == s.ToLowerInvariant())
                {
                    return true;
                }
            }
            return false;
        }
    }
}
