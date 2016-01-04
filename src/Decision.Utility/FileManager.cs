using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decision.Utility
{
    public static class FileManager
    {
        public static byte[] ConvertToByteArrary(this Stream stream, int length)
        {
            var fileData = new byte[length];
            stream.Read(fileData, 0, Convert.ToInt32(length));
            return fileData;
        }
    }
}
