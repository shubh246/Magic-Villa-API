using MagivVilla_Web.Models;
using System.Linq.Expressions;

namespace MagivVilla_Web.Service
{
    public interface IVillaNumberService
    {
        Task<T> CreateAsync<T>(VillaNumberCreateDto dto);
        Task<T> DeleteAsync<T>(int id);
        Task<T> UpdateAsync<T>(VillaNumberUpdateDto dto);
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
    }
}
