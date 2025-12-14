using Domain.Contracts;
using Domain.Entites;
using Periestence.Data.Contexts;
using Periestence.Repositories;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Periestence
{
    public class UnitOfWork(StoreDbContext _context) : IUnitOfWork
    {
        private ConcurrentDictionary<string, object> _repositories = new ConcurrentDictionary<string, object>();
        //public IGenericRepository<TKey, TEntity> GetRepository<TKey, TEntity>() where TEntity : BaseEntity<TKey>
        //{
        //  var key = typeof(TEntity).Name;

        //    if (!_repositories.ContainsKey(key))
        //    {
        //        var Repository = new GenericRepository<TKey, TEntity>(_context);
        //        _repositories.Add(key, Repository);
        //    }

        //    return (IGenericRepository<TKey, TEntity>) _repositories[key];

        //}

        public IGenericRepository<TKey, TEntity> GetRepository<TKey, TEntity>() where TEntity : BaseEntity<TKey>
        {
           return (IGenericRepository<TKey, TEntity>) _repositories.GetOrAdd(typeof(TEntity).Name, new GenericRepository<TKey, TEntity>(_context));

           

        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
