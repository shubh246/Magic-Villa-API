using MagicVilla_Utility;
using MagivVilla_Web.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http.Headers;
using System.Text;

namespace MagivVilla_Web.Service
{
    public class BaseService : IService
    {
        public ApiResponse responseMode { get; set; }
        public IHttpClientFactory HttpClient { get; set; }   
        public BaseService(IHttpClientFactory HttpClient)
        {
            this.responseMode = new();
            this.HttpClient = HttpClient;
        }
        public async Task<T> SendAsync<T>(ApiRequest apiRequest)
        {
            try
            {
                var client = HttpClient.CreateClient("MagicApi");
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(apiRequest.ApiUrl);
                if (apiRequest.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data), Encoding.UTF8, "application/json");
                }
                switch (apiRequest.ApiType)
                {
                    case SD.ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case SD.ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case SD.ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;

                }
                HttpResponseMessage apiResponse = null;
                if (!string.IsNullOrEmpty(apiRequest.Token))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiRequest.Token);
                }
                apiResponse = await client.SendAsync(message);
                var apiContent = await apiResponse.Content.ReadAsStringAsync();
                try
                {
                    ApiResponse APiResponse = JsonConvert.DeserializeObject<ApiResponse>(apiContent);
                    if(apiResponse.StatusCode==System.Net.HttpStatusCode.BadRequest|| apiResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        APiResponse.StatusCode =System.Net.HttpStatusCode.BadRequest;
                        APiResponse.IsSuccess = false;
                        var res = JsonConvert.SerializeObject(APiResponse);
                        var retObj = JsonConvert.DeserializeObject<T>(res);
                        return retObj;

                    }
                }
                catch(Exception e)
                {
                    var exceptResponse = JsonConvert.DeserializeObject<T>(apiContent);
                    return exceptResponse;
                }
                var ApiResponse = JsonConvert.DeserializeObject<T>(apiContent);
                return ApiResponse;




            }
            catch (Exception e)
            {
                var dto = new ApiResponse {
                    ErrorMessage = new List<string>() { Convert.ToString(e.Message) },
                    IsSuccess = false };
                 var res = JsonConvert.SerializeObject(dto);
            var ApiResponse = JsonConvert.DeserializeObject<T>(res);
            return ApiResponse;


        }
        }
    }
}
