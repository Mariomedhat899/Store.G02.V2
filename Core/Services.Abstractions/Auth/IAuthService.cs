using Shared.Dtos.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions.Auth
{
    public interface IAuthService
    {
        Task<UserResponse?> LogInAsync(LogInRequest request);
        Task<UserResponse?> RegisterAsync(RegisterRequest request);
        Task DeleteAsync(string Email);
    }
}
