using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions.BadRequest
{
    public class BasketBadRequestException() : BadRequestException("Failed Operation When Creating Or Updating Basket")
    {
    }
}
