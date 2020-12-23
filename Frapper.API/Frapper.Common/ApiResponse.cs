using Newtonsoft.Json;
using System;

namespace Frapper.Common
{

    public class ApiResponse
    {
        public int StatusCode { get; private set; }
        public string StatusDescription { get; private set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Message { get; private set; }
        public object Result { get; set; }

        public ApiResponse(int statusCode, string statusDescription)
        {
            this.StatusCode = statusCode;
            this.StatusDescription = statusDescription;
        }

        public ApiResponse(int statusCode, string statusDescription, string message) : this(statusCode, statusDescription)
        {
            this.Message = message;
        }

        public ApiResponse(int statusCode, string statusDescription, string message, object result = null) : this(statusCode, statusDescription, message)
        {
            this.Result = result;
        }
    }
}
