using System.Net;

namespace Frapper.Common
{
    public class NotFoundResponse : ApiResponse
    {
        public NotFoundResponse()
            : base(404, HttpStatusCode.NotFound.ToString())
        {
        }


        public NotFoundResponse(string message)
            : base(404, HttpStatusCode.NotFound.ToString(), message)
        {
        }
    }
}