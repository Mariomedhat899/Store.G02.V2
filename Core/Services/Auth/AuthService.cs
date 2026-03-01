using AutoMapper;
using Domain.Entites.Identity;
using Domain.Exceptions.BadRequest;
using Domain.Exceptions.NotFound;
using Domain.Exceptions.UnAuthorized;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Abstractions.Auth;
using Shared;
using Shared.Dtos.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services.Auth
{
    public class AuthService(UserManager<AppUser> _userManager, IOptions<JwtOptions> _options, IMapper _mapper) : IAuthService
    {
        public async Task<bool> CheckEmailExistAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }

        public async Task<UserResponse?> GetCurrentUserAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null) throw new UserNotFoundException(email);

            return new UserResponse()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await GenerateTokenAsync(user)
            };
        }

        public async Task<AddressDto?> GetCurrentUserAddressAsync(string email)
        {
            // await _userManager.FindByEmailAsync(email); this function dose not load the navigation propertie 
            var user = await _userManager.Users.Include(U => U.Adress).FirstOrDefaultAsync(U => U.Email.ToLower() == email.ToLower());

            if (user is null) throw new UserNotFoundException(email);

            return _mapper.Map<AddressDto>(user.Adress);
        }

        public async Task<AddressDto?> UpdateCurrentUserAddressAsync(AddressDto request, string email)
        {
            var user = await _userManager.Users.Include(U => U.Adress).FirstOrDefaultAsync(U => U.Email.ToLower() == email.ToLower());

            if (user is null) throw new UserNotFoundException(email);

            if (user.Adress is null)
            {
                //Create New Address

                user.Adress = _mapper.Map<Address>(request);
            }
            else
            {
                //Update The Current Address

                user.Adress.FirstName = request.FirstName;
                user.Adress.LastName = request.LastName;
                user.Adress.Street = request.Street;
                user.Adress.City = request.City;
                user.Adress.Country = request.Country;
            }

            await _userManager.UpdateAsync(user);

            return _mapper.Map<AddressDto>(user.Adress);
        }

        public async Task<UserResponse?> LogInAsync(LogInRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null) throw new UserNotFoundException(request.Email);

            var flag = await _userManager.CheckPasswordAsync(user, request.Password);

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

            var jwtOptions = _options.Value;

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
