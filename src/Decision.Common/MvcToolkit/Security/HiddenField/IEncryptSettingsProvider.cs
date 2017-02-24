namespace NTierMvcFramework.Common.MvcToolkit.Security.HiddenField
{
    public interface IEncryptSettingsProvider
    {
        byte[] EncryptionKey { get; }
        string EncryptionPrefix { get; }
    }
}