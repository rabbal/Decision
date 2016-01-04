using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Decision.Common.MVC
{
    public static class PlayerHelper
    {

        #region AparatPlayer
        public static MvcHtmlString AparatPlayer(this HtmlHelper helper, string mediafile, int height, int width)
        {
            var player = @"<embed height=""{0}"" width=""{1}"" flashvars=""config=http://www.aparat.com//video/video/config/videohash/{2}/watchtype/embed"" 
                                allowfullscreen=""true"" 
                                quality=""high"" 
                                name=""aparattv_{2}"" id=""aparattv_{2}"""" src=""http://host10.aparat.com/public/player/aparattv"" 
                                type=""application/x-shockwave-flash"">";

            player = string.Format(player, height, width, mediafile);
            return new MvcHtmlString(player);
        }
        #endregion


        #region YouTubePalyer
        //usage 
        //@Html.YouTubePlayer("Casablanca", "iLdqKUkkM6w", new YouTubePlayerOption()
        //                         {
        //                             Border = true
        //                         })
        public class YouTubePlayerOption
        {
            int _width = 425;
            int _height = 355;
            Color _color1 = Color.Black;
            Color _color2 = Color.Aqua;

            public YouTubePlayerOption()
            {
                Border = false;
            }

            public int Width { get { return _width; } set { _width = value; } }
            public int Height { get { return _height; } set { _height = value; } }
            public Color PrimaryColor { get { return _color1; } set { _color1 = value; } }
            public Color SecondaryColor { get { return _color2; } set { _color2 = value; } }
            public bool Border { get; set; }
        }

        public class ConvertColorToHexa
        {
            private static readonly char[] HexDigits =
            {
                '0', '1', '2', '3', '4', '5', '6', '7',
                '8', '9', 'A', 'B', 'C', 'D', 'E', 'F'
            };

            public static string ConvertColorToHexaString(Color color)
            {
                var bytes = new byte[3];
                bytes[0] = color.R;
                bytes[1] = color.G;
                bytes[2] = color.B;
                var chars = new char[bytes.Length * 2];
                for (var i = 0; i < bytes.Length; i++)
                {
                    int b = bytes[i];
                    chars[i * 2] = HexDigits[b >> 4];
                    chars[i * 2 + 1] = HexDigits[b & 0xF];
                }
                return new string(chars);
            }


        }
        public static MvcHtmlString YouTubePlayer(this HtmlHelper helper, string playerId, string mediaFile, YouTubePlayerOption youtubePlayerOption)
        {

            const string baseURL = "http://www.youtube.com/v/";

            // YouTube Embedded Code
            var player = @"<div id=""YouTubePlayer_{7}""width:{1}px; height:{2}px;"">
                                 <object width=""{1}"" height=""{2}"">
                                 <param name=""movie"" value=""{6}{0}&fs=1&border={3}&color1={4}&color2={5}""></param>
                                 <param name=""allowFullScreen"" value=""true""></param>
                                 <embed src=""{6}{0}&fs=1&border={3}&color1={4}&color2={5}""
                                 type = ""application/x-shockwave-flash""
                                 width=""{1}"" height=""{2}"" allowfullscreen=""true""></embed>
                                 </object>
                             </div>";

            // Replace All The Value
            player = String.Format(player, mediaFile, youtubePlayerOption.Width, youtubePlayerOption.Height, (youtubePlayerOption.Border ? "1" : "0"), ConvertColorToHexa.ConvertColorToHexaString(youtubePlayerOption.PrimaryColor), ConvertColorToHexa.ConvertColorToHexaString(youtubePlayerOption.SecondaryColor), baseURL, playerId);

            //Retrun Embedded Code
            return new MvcHtmlString(player);
        }
        #endregion

    }
}
