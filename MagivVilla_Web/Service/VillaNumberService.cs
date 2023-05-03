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
    
        public Task<T> CreateAsync<T>(VillaNumberCreateDto dto)
        {

            return SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                ApiUrl = villaUrl + "/api/villaNumberAPI/"

            });
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.DELETE,
                ApiUrl = villaUrl + "/api/villaNumberAPI/" + id

            });
        }

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                ApiUrl = villaUrl + "/api/villaNumberAPI/"

            }); 
        }

        public Task<T> GetAsync<T>(int id)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                ApiUrl = villaUrl + "/api/villaNumberAPI/" + id

            });
        }

        public Task<T> UpdateAsync<T>(VillaNumberUpdateDto dto)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                ApiUrl = villaUrl + "/api/villaNumberAPI/" + dto.VillaNo

            });
        }
    }
}
