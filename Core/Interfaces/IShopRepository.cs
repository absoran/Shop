using Core.Entities;
using Core.Spesifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace Core.Interfaces
{
    public interface IShopRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<T> GetByNameAsync(string name);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(T entity);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<T> GetEntityWithSpec(ISpecification<T> spec);
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
        Task<int> CounAsync(ISpecification<T> spec);
        // This is for tracking in memory in EF. Hence, kept synchronous.
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
