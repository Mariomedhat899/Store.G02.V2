using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions.NotFound
{
    public class BasketNotFoundExeption(string id) :
        NotFoundException($"Basket With Id {id} Was Not Found !!")
    {
    }
}
