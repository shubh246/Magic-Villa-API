using static MagicVilla_Utility.SD;

namespace MagivVilla_Web.Models
{
    public class ApiRequest
    {
        public ApiType ApiType { get; set; } = ApiType.GET;
        public string ApiUrl { get; set; } 
        public Object Data { get; set; }
    }
}
