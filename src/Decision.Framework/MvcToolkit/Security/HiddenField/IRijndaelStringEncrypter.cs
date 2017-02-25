using System;

namespace Decision.Framework.MvcToolkit.Security.HiddenField
{
    public interface IRijndaelStringEncrypter : IDisposable
    {
        string Encrypt(string value);
        string Decrypt(string value);
    }
}
