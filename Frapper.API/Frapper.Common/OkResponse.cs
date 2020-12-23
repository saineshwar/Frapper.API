using System.Net;

namespace Frapper.Common
{
    public class OkResponse : ApiResponse
    {
        public OkResponse()
            : base(200, HttpStatusCode.OK.ToString())
        {
        }


        public OkResponse(string message)
            : base(200, HttpStatusCode.OK.ToString(), message)
        {
        }

        public OkResponse(string message, object result)
          : base(200, HttpStatusCode.OK.ToString(), message, result)
        {
        }
    }
}