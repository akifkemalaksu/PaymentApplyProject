using PaymentApplyProject.Application.Dtos.CallbackDtos;
using System.Security.Cryptography;
using System.Text;

namespace PaymentApplyProject.Application.Helpers
{
    public static class GeneratorHelper
    {
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

            Random Random = new();

            char[] password = new char[length];
            for (int i = 0; i < length; i++)
            {
                password[i] = validChars[Random.Next(0, validChars.Length)];
            }

            return new string(password);
        }

        public static string GenerateSha256Key(string rawData)
        {
            using SHA256 sha256Hash = SHA256.Create();
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

            StringBuilder builder = new();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }

        public static string GenerateDataHashForCallback(GenerateHashDto generateHash)
        {
            var dataToHash = $"{generateHash.Token}-{generateHash.TransactionId}-{generateHash.Amount:0.##}-{generateHash.Status}";

            byte[] key = Encoding.UTF8.GetBytes(generateHash.Password);
            byte[] data = Encoding.UTF8.GetBytes(dataToHash);

            using HMACSHA256 hmac = new(key);
            byte[] hash = hmac.ComputeHash(data);
            string result = BitConverter.ToString(hash).Replace("-", "").ToLower();

            return result;
        }
    }
}
