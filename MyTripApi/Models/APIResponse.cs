using System.Net;

namespace MyTripApi.Models
{
    public class APIResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public List<string>?  ErroMessages { get; set; }
        public object? Result { get; set; }
    }
}
