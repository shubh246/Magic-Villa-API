using MagicVilla_Utility;
using MagivVilla_Web.Models;

namespace MagivVilla_Web.Service
{
    public class VillaNumberService : BaseService, IVillaNumberService { 
        private readonly IHttpClientFactory clientfactory;
        public string villaUrl;
        public VillaNumberService(IHttpClientFactory _clientfactory,IConfiguration configuration) : base(_clientfactory)
        {
            clientfactory = _clientfactory;
            villaUrl = configuration.GetValue<string>("ServiceUrls:VillaApi");
        }
    
        public Task<T> CreateAsync<T>(VillaNumberCreateDto dto, string token)
        {

            return SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                ApiUrl = villaUrl + "/api/villaNumberAPI/",
                Token = token

            });
        }

        public Task<T> DeleteAsync<T>(int id, string token)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.DELETE,
                ApiUrl = villaUrl + "/api/villaNumberAPI/" + id,
                Token = token

            });
        }

        public Task<T> GetAllAsync<T>(string token)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                ApiUrl = villaUrl + "/api/villaNumberAPI/",
                Token = token

            }); 
        }

        public Task<T> GetAsync<T>(int id, string token)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                ApiUrl = villaUrl + "/api/villaNumberAPI/" + id,
                Token = token

            });
        }

        public Task<T> UpdateAsync<T>(VillaNumberUpdateDto dto, string token)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                ApiUrl = villaUrl + "/api/villaNumberAPI/" + dto.VillaNo,
                Token = token

            });
        }
    }
}
