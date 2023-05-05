using MagicVilla_Utility;
using MagivVilla_Web.Models;

namespace MagivVilla_Web.Service
{
    public class AuthService: BaseService, IAuthService
    {
        private readonly IHttpClientFactory clientfactory;
        public string villaUrl;
        public AuthService(IHttpClientFactory _clientfactory, IConfiguration configuration) : base(_clientfactory)
        {
            clientfactory = _clientfactory;
            villaUrl = configuration.GetValue<string>("ServiceUrls:VillaApi");
        }

        public Task<T> LoginAsync<T>(LoginRequestDto obj)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = obj,
                ApiUrl = villaUrl + "/api/UserAuth/login"

            });
        }

        public Task<T> RegisterAsync<T>(RegistrationRequestDto obj)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = obj,
                ApiUrl = villaUrl + "/api/UserAuth/register"

            });
        }
    }
}
