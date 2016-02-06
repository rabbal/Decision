using System.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace Decision.Common.HiddenField
{
    public class EncryptSettingsProvider : IEncryptSettingsProvider
    {
        private readonly string _encryptionPrefix;
        private readonly byte[] _encryptionKey;

        public EncryptSettingsProvider()
        {
            //read settings from configuration
            var useHashingString = ConfigurationManager.AppSettings["UseHashingForEncryption"];
            var useHashing = System.String.Compare(useHashingString, "false", System.StringComparison.OrdinalIgnoreCase) != 0;

            _encryptionPrefix = ConfigurationManager.AppSettings["EncryptionPrefix"];
            if (string.IsNullOrWhiteSpace(_encryptionPrefix))
            {
                _encryptionPrefix = "encryptedHidden_";
            }

            var key = ConfigurationManager.AppSettings["EncryptionKey"];
            if (useHashing)
            {
                var hash = new SHA256Managed();
                _encryptionKey = hash.ComputeHash(Encoding.UTF8.GetBytes(key));
                hash.Clear();
                hash.Dispose();
            }
            else
            {
                _encryptionKey = Encoding.UTF8.GetBytes(key);
            }
        }

        #region ISettingsProvider Members

        public byte[] EncryptionKey
        {
            get
            {
                return _encryptionKey;
            }
        }

        public string EncryptionPrefix
        {
            get { return _encryptionPrefix; }
        }

        #endregion

    }
}