using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions.Cache
{
    public interface ICacheService
    {
       Task SetAsync(string Key,object Value,TimeSpan Duration);
       Task<string?> GetAsync(string Key);
    }
}
