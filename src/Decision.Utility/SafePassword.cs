using System.Collections.Generic;
using System.Linq;

namespace Decision.Utility
{
    public static class SafePassword
    {
        public static ISet<string> BadPasswords = new HashSet<string>
        {
            "password",
            "password1",
            "123456",
            "12345678",
            "1234",
            "qwerty",
            "12345",
            "dragon",
            "******",
            "baseball",
            "football",
            "letmein",
            "monkey",
            "696969",
            "abc123",
            "mustang",
            "michael",
            "shadow",
            "master",
            "jennifer",
            "111111",
            "2000",
            "jordan",
            "superman",
            "harley",
            "1234567",
            "iloveyou",
            "trustno1",
            "sunshine",
            "123123",
            "welcome"
        };
        public static bool IsSafePasword(this string data)
        {
            if (string.IsNullOrWhiteSpace(data)) return false;
            if (data.Length < 5) return false;
            if (BadPasswords.Contains(data.ToLowerInvariant())) return false;
            if (!HasDigitOrLowerOrUperCase(data)) return false;
            return !data.AreAllCharsEuqal();
        }

        public static bool AreAllCharsEuqal(this string data)
        {
            if (string.IsNullOrWhiteSpace(data)) return false;
            data = data.ToLowerInvariant();
            var firstElement = data.ElementAt(0);
            var euqalCharsLen = data.ToCharArray().Count(x => x == firstElement);
            return euqalCharsLen == data.Length;
        }

        public static bool HasDigitOrLowerOrUperCase(this string data)
        {
            return data.ToCharArray().Any(a => a.IsDigit() || a.IsLower() || a.IsUpper());
        }
        private static bool IsDigit(this char c)
        {
            if (c >= 48)
                return c <= 57;
            return false;
        }

        private static bool IsLower(this char c)
        {
            if (c >= 97)
                return c <= 122;
            return false;
        }


        private static bool IsUpper(this char c)
        {
            if (c >= 65)
                return c <= 90;
            return false;
        }
    }
}
