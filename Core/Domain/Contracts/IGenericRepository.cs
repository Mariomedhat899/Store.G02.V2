using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IGenericRepository<TKey, TEntity> where TEntity : BaseEntity<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync(bool ChangeTracker = false);
        Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TKey,TEntity> spec);
       Task<TEntity?> GetAsync(TKey key);
       Task<TEntity?> GetAsync(ISpecifications<TKey, TEntity> spec);
        Task AddAsync(TEntity entity);

        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
