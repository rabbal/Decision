namespace Decision.Utility.MimeTypeHelpers
{
    public sealed class AttachmentType : IAttachmentType
    {
        private static readonly IAttachmentType unknownMime =
            new AttachmentType("application/octet-stream", "Unknown", string.Empty);

        private static readonly IAttachmentType photo = new AttachmentType("image/png", "Photo", ".jpg");

        private static readonly IAttachmentType video = new AttachmentType("video/mp4", "Video", ".mp4");

        private static readonly IAttachmentType document = new AttachmentType("application/pdf", "Document", ".pdf");

        private static readonly IAttachmentType unknown = new AttachmentType(string.Empty, "Unknown", string.Empty);

        private readonly string mimeType;

        private readonly string friendlyName;

        private readonly string extension;

        // Possibly make this private if you only use the static predefined MIME types.
        public AttachmentType(string mimeType, string friendlyName, string extension)
        {
            this.mimeType = mimeType;
            this.friendlyName = friendlyName;
            this.extension = extension;
        }

        public static IAttachmentType UnknownMime
        {
            get
            {
                return unknownMime;
            }
        }

        public static IAttachmentType Photo
        {
            get
            {
                return photo;
            }
        }

        public static IAttachmentType Video
        {
            get
            {
                return video;
            }
        }

        public static IAttachmentType Document
        {
            get
            {
                return document;
            }
        }

        public static IAttachmentType Unknown
        {
            get
            {
                return unknown;
            }
        }

        public string MimeType
        {
            get
            {
                return mimeType;
            }
        }

        public string FriendlyName
        {
            get
            {
                return friendlyName;
            }
        }

        public string Extension
        {
            get
            {
                return extension;
            }
        }
    }
}
