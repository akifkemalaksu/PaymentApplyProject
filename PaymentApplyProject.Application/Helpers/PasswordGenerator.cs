using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Application.Helpers
{
    public static class PasswordGenerator
    {
        private static readonly Random Random = new Random();

        public static string GeneratePassword(int length = 10, bool includeLowercase = true, bool includeUppercase = true,
            bool includeNumbers = true)
        {
            const string lowercaseChars = "abcdefghijklmnopqrstuvwxyz";
            const string uppercaseChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string numberChars = "0123456789";

            string validChars = string.Empty;
            if (includeLowercase)
                validChars += lowercaseChars;
            if (includeUppercase)
                validChars += uppercaseChars;
            if (includeNumbers)
                validChars += numberChars;

            char[] password = new char[length];
            for (int i = 0; i < length; i++)
            {
                password[i] = validChars[Random.Next(0, validChars.Length)];
            }

            return new string(password);
        }
    }
}
