using System;
using System.IO;
using System.Net;
using System.Net.Http;

namespace Decision.Framework.WebAPIToolkit
{
    public class VideoStream
    {
        private readonly string _filename;

        public VideoStream(string videoPath)
        {
            _filename = videoPath;
            using (var video = File.Open(_filename, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                FileLength = video.Length;
            }
        }

        public long FileLength { get; private set; }

        public async void WriteToStream(Stream outputStream,
            HttpContent content, TransportContext context)
        {
            try
            {
                var buffer = new byte[65536];

                using (var video = File.Open(_filename, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    var length = (int) video.Length;
                    var bytesRead = 1;

                    while (length > 0 && bytesRead > 0)
                    {
                        bytesRead = video.Read(buffer, 0, Math.Min(length, buffer.Length));
                        await outputStream.WriteAsync(buffer, 0, bytesRead);
                        length -= bytesRead;
                    }
                }
            }
            finally
            {
                outputStream.Close();
            }
        }
    }
}