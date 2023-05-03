using MagivVilla_Web.Models;

namespace MagivVilla_Web.Service
{
    public interface IService
    {
        ApiResponse responseMode { get; set; }
        Task<T> SendAsync<T>(ApiRequest apiRequest);
    }
}
