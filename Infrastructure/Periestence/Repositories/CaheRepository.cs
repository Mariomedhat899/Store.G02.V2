using Domain.Contracts;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Periestence.Repositories
{
    public class CaheRepository(IConnectionMultiplexer _multiplexer) : ICacheRepository
    {
        private readonly IDatabase _database = _multiplexer.GetDatabase();
        public async Task<string?> GetAsync(string Key)
        {
           return await _database.StringGetAsync(Key);
        }

        public async Task SetAsync(string Key, object Value, TimeSpan Duration)
        {
           await _database.StringSetAsync(Key, JsonSerializer.Serialize(Value), Duration);
        }
    }
}
