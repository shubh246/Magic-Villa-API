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
    
        public Task<T> CreateAsync<T>(VillaCreateDto dto, string token)
        {

            return SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                ApiUrl = villaUrl + "/api/villaAPI/",
                Token = token

            });
        }

        public Task<T> DeleteAsync<T>(int id, string token)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.DELETE,
                ApiUrl = villaUrl + "/api/villaAPI/" +id,
                Token = token

            });
        }

        public Task<T> GetAllAsync<T>(string token)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                ApiUrl = villaUrl + "/api/villaAPI/" ,
                Token = token

            }); 
        }

        public Task<T> GetAsync<T>(int id, string token)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                ApiUrl = villaUrl + "/api/villaAPI/" + id,
                Token = token

            });
        }

        public Task<T> UpdateAsync<T>(VillaUpdateDto dto, string token)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                ApiUrl = villaUrl + "/api/villaAPI/" + dto.Id,
                Token=token

            });
        }
    }
}
