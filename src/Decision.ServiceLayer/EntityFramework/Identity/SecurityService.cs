using System;
using System.Security.Cryptography;
using System.Text;
using Decision.ServiceLayer.Interfaces.Identity;

namespace Decision.ServiceLayer.EntityFramework.Identity
{
    public class SecurityService : ISecurityService
    {
        #region Public Methods
        public string GetSha256Hash(string input)
        {
            using (var hashAlgorithm = new SHA256CryptoServiceProvider())
            {
                var byteValue = Encoding.UTF8.GetBytes(input);
                var byteHash = hashAlgorithm.ComputeHash(byteValue);
                return Convert.ToBase64String(byteHash);
            }
        }

        #endregion
    }
}