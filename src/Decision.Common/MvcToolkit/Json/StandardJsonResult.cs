using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace NTierMvcFramework.Common.MvcToolkit.Json
{
    public class StandardJsonResult : JsonNetResult
    {
        #region Properties (1)
        public IList<string> ErrorMessages { get; private set; }
        #endregion

        #region Constructor (1)
        public StandardJsonResult()
        {
            ErrorMessages = new List<string>();

            Settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Converters = new JsonConverter[]
               {
                   new StringEnumConverter()
               }
                
            };
        }
        #endregion

        #region Methods (2)
       
        public void AddError(string errorMessage)
        {
            ErrorMessages.Add(errorMessage);
        }

        protected override void SerializeData(HttpResponseBase response)
        {
            if (ErrorMessages.Any())
            {
                var originalData = Data;
                Data = new
                {
                    Success = false,
                    OriginalData = originalData,
                    ErrorMessage = string.Join("/n", ErrorMessages),
                    ErrorMessages = ErrorMessages
                };

                response.StatusCode = 400;
            }

            base.SerializeData(response);
        }

        #endregion
    }

    public class StandardJsonResult<T> : StandardJsonResult
    {
        public new T Data
        {
            get { return (T)base.Data; }
            set { base.Data = value; }
        }
    }
}