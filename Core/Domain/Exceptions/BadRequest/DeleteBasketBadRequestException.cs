using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions.BadRequest
{
    public class DeleteBasketBadRequestException(string id) : BadRequestException($"Invalid Operation While Deleting Basket With Id {id}")
    {
    }
}
