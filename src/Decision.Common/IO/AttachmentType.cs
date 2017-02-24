namespace NTierMvcFramework.Common.IO
{
    public sealed class AttachmentType : IAttachmentType
    {
        // Possibly make this private if you only use the static predefined MIME types.
        public AttachmentType(string mimeType, string friendlyName, string extension)
        {
            MimeType = mimeType;
            FriendlyName = friendlyName;
            Extension = extension;
        }

        public static IAttachmentType UnknownMime { get; } = new AttachmentType("application/octet-stream",
            nameof(Unknown), string.Empty);

        public static IAttachmentType Photo { get; } = new AttachmentType("image/png", nameof(Photo), ".jpg");

        public static IAttachmentType Video { get; } = new AttachmentType("video/mp4", nameof(Video), ".mp4");

        public static IAttachmentType Document { get; } = new AttachmentType("application/pdf", nameof(Document), ".pdf")
            ;

        public static IAttachmentType Unknown { get; } = new AttachmentType(string.Empty, nameof(Unknown), string.Empty)
            ;

        public string MimeType { get; }

        public string FriendlyName { get; }

        public string Extension { get; }
    }
}