using System.Net;

namespace Magic_Villa.Models
{
    public class ApiResponse

    {
        public ApiResponse() {
            ErrorMessage=new List<string>();
          }
        public HttpStatusCode StatusCode { get; set; }
        public List<string> ErrorMessage { get; set; }
        public bool IsSuccess { get; set; } = true;
        public Object Result { get; set; }

    }
}
