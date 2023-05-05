using MagivVilla_Web.Models;

namespace MagivVilla_Web.Service
{
    public interface IAuthService
    {
        Task<T> LoginAsync<T>(LoginRequestDto objCreate);
        Task<T> RegisterAsync<T>(RegistrationRequestDto objCreate);


    }
}
