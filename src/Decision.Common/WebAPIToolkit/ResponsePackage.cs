using System.Collections.Generic;

namespace NTierMvcFramework.Common.WebAPIToolkit
{
    public class ResponsePackage
    {
        public ResponsePackage(object result, List<string> errors)
        {
            Errors = errors;
            Result = result;
        }

        public List<string> Errors { get; set; }

        public object Result { get; set; }
    }
}