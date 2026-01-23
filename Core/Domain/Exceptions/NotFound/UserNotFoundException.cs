using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions.NotFound
{
    public class UserNotFoundException(string Email) : NotFoundException($"User with email '{Email}' was not found !! ")
    {
    }
}
