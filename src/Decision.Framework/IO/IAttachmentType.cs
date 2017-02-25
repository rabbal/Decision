namespace Decision.Framework.IO
{
    public interface IAttachmentType
    {
        string MimeType { get; }

        string FriendlyName { get; }

        string Extension { get; }
    }
}