using System.IO;
using Newtonsoft.Json;

namespace NTierMvcFramework.Common.JsonToolkit
{
    public static class JsonHelpers
    {

       
        public static T DeserializeFromFile<T>(string filePath, JsonSerializerSettings settings = null)
        {
            if (!File.Exists(filePath))
                return default(T);

            using (var fileStream = File.OpenRead(filePath))
            {
                using (var streamReader = new StreamReader(fileStream))
                {
                    using (var reader = new JsonTextReader(streamReader))
                    {
                        var serializer = settings == null ? JsonSerializer.Create() : JsonSerializer.Create(settings);
                        return serializer.Deserialize<T>(reader);
                    }
                }
            }
        }

        public static void SerializeToFile(string filePath, object data, JsonSerializerSettings settings = null)
        {
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                using (var streamReader = new StreamWriter(fileStream))
                {
                    using (var reader = new JsonTextWriter(streamReader))
                    {
                        var serializer = settings == null ? JsonSerializer.Create() : JsonSerializer.Create(settings);
                        serializer.Serialize(reader, data);
                    }
                }
            }
        }
    }
}