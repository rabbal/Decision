using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;

namespace NTierMvcFramework.Common.WebAPIToolkit.DelegatingHandlers
{
    public class ResponseWrappingHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);

            return BuildApiResponse(request, response);
        }

        private static HttpResponseMessage BuildApiResponse(HttpRequestMessage request, HttpResponseMessage response)
        {
            object content;
            var modelStateErrors = new List<string>();

            //Step 2: Get the Response Content
            if (response.TryGetContentValue(out content) && !response.IsSuccessStatusCode)
            {
                var error = content as HttpError;
                if (error != null)
                {
                    //Step 3: If content is an error, return nothing for the Result.
                    content = null;

                    //Step 4: Insert the ModelState errors              
                    if (error.ModelState != null)
                    {
                        var httpErrorObject = response.Content.ReadAsStringAsync().Result;

                        var anonymousErrorObject = new {message = "", ModelState = new Dictionary<string, string[]>()};

                        var deserializedErrorObject = JsonConvert.DeserializeAnonymousType(httpErrorObject,
                            anonymousErrorObject);

                        var modelStateValues =
                            deserializedErrorObject.ModelState.Select(kvp => string.Join(". ", kvp.Value));

                        var stateValues = modelStateValues as string[] ?? modelStateValues.ToArray();
                        for (var i = 0; i < stateValues.Count(); i++)
                        {
                            modelStateErrors.Add(stateValues.ElementAt(i));
                        }
                    }
                }
            }

            //Step 5: Create a new response
            var newResponse = request.CreateResponse(response.StatusCode, new ResponsePackage(content, modelStateErrors));

            //Step 6: Add Back the Response Headers
            foreach (var header in response.Headers)
            {
                newResponse.Headers.Add(header.Key, header.Value);
            }

            return newResponse;
        }
    }
}