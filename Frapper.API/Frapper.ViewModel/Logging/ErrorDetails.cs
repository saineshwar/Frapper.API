using Newtonsoft.Json;

namespace Frapper.ViewModel.Logging
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}