using Domain.Contracts;
using Services.Abstractions.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Cahce
{
    public class CacheService(ICacheRepository _cacheRepository) : ICacheService
    {
        public async Task<string?> GetAsync(string Key)
        {
          return await _cacheRepository.GetAsync(Key);
        }

        public async Task SetAsync(string Key, object Value, TimeSpan Duration)
        {
           await _cacheRepository.SetAsync(Key, Value, Duration);
        }
    }
}
