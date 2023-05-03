using MagicVilla_Utility;
using MagivVilla_Web.Models;

namespace MagivVilla_Web.Service
{
    public class VillaService : BaseService, IVillaService { 
        private readonly IHttpClientFactory clientfactory;
        public string villaUrl;
        public VillaService(IHttpClientFactory _clientfactory,IConfiguration configuration) : base(_clientfactory)
        {
            clientfactory = _clientfactory;
            villaUrl = configuration.GetValue<string>("ServiceUrls:VillaApi");
        }
    
        public Task<T> CreateAsync<T>(VillaCreateDto dto)
        {

            return SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                ApiUrl = villaUrl + "/api/villaAPI/"

            });
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.DELETE,
                ApiUrl = villaUrl + "/api/villaAPI/" +id

            });
        }

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                ApiUrl = villaUrl + "/api/villaAPI/" 

            }); 
        }

        public Task<T> GetAsync<T>(int id)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                ApiUrl = villaUrl + "/api/villaAPI/" + id

            });
        }

        public Task<T> UpdateAsync<T>(VillaUpdateDto dto)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                ApiUrl = villaUrl + "/api/villaAPI/" + dto.Id

            });
        }
    }
}
