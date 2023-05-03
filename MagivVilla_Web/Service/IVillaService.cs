using MagivVilla_Web.Models;
using System.Linq.Expressions;

namespace MagivVilla_Web.Service
{
    public interface IVillaService
    {
        Task<T> CreateAsync<T>(VillaCreateDto dto);
        Task<T> DeleteAsync<T>(int id);
        Task<T> UpdateAsync<T>(VillaUpdateDto dto);
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
    }
}
