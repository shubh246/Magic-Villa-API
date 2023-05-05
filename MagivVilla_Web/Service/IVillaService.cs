using MagivVilla_Web.Models;
using System.Linq.Expressions;

namespace MagivVilla_Web.Service
{
    public interface IVillaService
    {
        Task<T> CreateAsync<T>(VillaCreateDto dto,string token);
        Task<T> DeleteAsync<T>(int id, string token);
        Task<T> UpdateAsync<T>(VillaUpdateDto dto, string token);
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(int id, string token);
    }
}
