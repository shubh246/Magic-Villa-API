using MagivVilla_Web.Models;
using System.Linq.Expressions;

namespace MagivVilla_Web.Service
{
    public interface IVillaNumberService
    {
        Task<T> CreateAsync<T>(VillaNumberCreateDto dto, string token);
        Task<T> DeleteAsync<T>(int id, string token);
        Task<T> UpdateAsync<T>(VillaNumberUpdateDto dto, string token);
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(int id, string token);
    }
}
