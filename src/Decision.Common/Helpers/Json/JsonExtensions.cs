using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Json;
using Newtonsoft.Json;

namespace Decision.Common.Helpers.Json
{
    public static class JsonExtensions
    {
        #region ToJson
        public static string StringArrayToJson(this string[] array)
        {
            if (array == null || !array.Any())
                return "[]";
            else
                return Newtonsoft.Json.JsonConvert.SerializeObject(array);
        }
        #endregion
    }
}
