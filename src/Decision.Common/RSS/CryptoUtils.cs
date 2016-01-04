using System;
using System.Security.Cryptography;
using System.Text;

namespace Decision.Common.RSS
{
    public static class CryptoUtils
    {
        public static string Sha1(this string data)
        {
            using (var sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(data));
                return BitConverter.ToString(hash).Replace("-", "").ToUpperInvariant();
            }
        }
    }
}