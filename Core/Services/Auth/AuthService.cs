using Domain.Entites.Identity;
using Domain.Exceptions.BadRequest;
using Domain.Exceptions.NotFound;
using Domain.Exceptions.UnAuthorized;
using Microsoft.AspNetCore.Identity;
using Services.Abstractions.Auth;
using Shared.Dtos.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Auth
{
    public class AuthService(UserManager<AppUser> _userManager) : IAuthService
    {
        public async Task<UserResponse?> LogInAsync(LogInRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null) throw new UserNotFoundException(request.Email);

          var flag = await  _userManager.CheckPasswordAsync(user, request.Password);

            if (!flag) throw new UnAuthorizedException();

            return new UserResponse()
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = "ToDo"
            };


        }

        public async Task<UserResponse?> RegisterAsync(RegisterRequest request)
        {
            var user = new AppUser()
            {
                DisplayName = request.DisplayName,
                Email = request.Email,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber

            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded) throw new RegistrationBadRequestException(result.Errors.Select(E => E.Description).ToList());


            return new UserResponse()
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = "ToDo"
            };
        }

        public async Task DeleteAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null) throw new UserNotFoundException(email);

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded) throw new Exception("Attemped Failed to Delete This User");


            
        }
    }
}
