using System.Drawing;

namespace Decision.Common.MvcToolkit.Video
{
    public class YouTubePlayerOption
    {
        public YouTubePlayerOption()
        {
            Border = false;
        }

        public int Width { get; set; } = 425;
        public int Height { get; set; } = 355;
        public Color PrimaryColor { get; set; } = Color.Black;
        public Color SecondaryColor { get; set; } = Color.Aqua;
        public bool Border { get; set; }
    }
}