using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions.UnAuthorized
{
    public class UnAuthorizedException() : Exception("You are not authorized to perform this action !! ")
    {
    }
}
