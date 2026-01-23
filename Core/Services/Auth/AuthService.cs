using Domain.Entites.Identity;
using Domain.Exceptions.BadRequest;
using Domain.Exceptions.NotFound;
using Domain.Exceptions.UnAuthorized;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Abstractions.Auth;
using Shared;
using Shared.Dtos.Auth;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.Auth
{
    public class AuthService(UserManager<AppUser> _userManager,IOptions<JwtOptions> _options) : IAuthService
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
                Token = await GenerateTokenAsync(user)
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
                Token = await GenerateTokenAsync(user)
            };
        }

        public async Task DeleteAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null) throw new UserNotFoundException(email);

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded) throw new Exception("Attemped Failed to Delete This User");


            
        }


        private async Task<string> GenerateTokenAsync(AppUser user)
        {
            //Token:
            //Header(Type,Algo)
            //PayLoad(Claims)
            //Signature(Key)
            
            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.GivenName,user.DisplayName),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.MobilePhone,user.PhoneNumber)
                

            };
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var jwtOptions =_options.Value; 

            //StrongSecurityKeyForAuthenticationStrongSecurityKeyForAuthenticationStrongSecurityKeyForAuthenticationStrongSecurityKeyForAuthentication
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey));

                var token = new JwtSecurityToken(
                issuer: jwtOptions.Issuer,
                audience: jwtOptions.Audience,
                claims: authClaims,
                expires: DateTime.Now.AddMinutes(jwtOptions.ExpirationInMinutes),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
           

        }
    }
}
