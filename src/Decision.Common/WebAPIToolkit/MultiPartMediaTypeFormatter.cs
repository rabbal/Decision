using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;
using Decision.Common.Infrastructure;

namespace Decision.Common.WebAPIToolkit
{
    public class MultiPartMediaTypeFormatter : MediaTypeFormatter
    {
        private const string SupportedMediaType = "multipart/form-data";

        public MultiPartMediaTypeFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue(SupportedMediaType));
        }
        
        public override bool CanReadType(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            return typeof(INeedMultiPartMediaTypeFormatter).IsAssignableFrom(type); ;
        }

        public override bool CanWriteType(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            return false;
        }
        public override async Task<object> ReadFromStreamAsync(Type type, Stream stream, HttpContent content,
            IFormatterLogger formatterLogger)
        {
            var provider = await content.ReadAsMultipartAsync();
            var formData = provider.Contents.ToList();

            var modelInstance = Activator.CreateInstance(type);
            IEnumerable<PropertyInfo> properties = type.GetProperties();
            foreach (var prop in properties)
            {
                var propName = prop.Name.ToLower();
                var propType = prop.PropertyType;

                var data = formData.FirstOrDefault(d => d.Headers.ContentDisposition.Name.ToLower().Contains(propName));
                if (data != null)
                {
                    if (data.Headers.ContentType == null) continue;
                    using (var fileStream = await data.ReadAsStreamAsync())
                    {
                        using (var ms = new MemoryStream())
                        {
                            fileStream.CopyTo(ms);
                            prop.SetValue(modelInstance, ms.ToArray());
                        }
                    }
                }
                else
                {
                    var rawVal = await data.ReadAsStringAsync();
                    var val = Convert.ChangeType(rawVal, propType);

                    prop.SetValue(modelInstance, val);
                }
            }
            return modelInstance;
        }
    }
}