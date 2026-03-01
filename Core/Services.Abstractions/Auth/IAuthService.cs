using Shared.Dtos.Auth;

namespace Services.Abstractions.Auth
{
    public interface IAuthService
    {
        Task<UserResponse?> LogInAsync(LogInRequest request);
        Task<UserResponse?> RegisterAsync(RegisterRequest request);
        Task DeleteAsync(string Email);

        Task<bool> CheckEmailExistAsync(string email);

        Task<UserResponse?> GetCurrentUserAsync(string email);
        Task<AddressDto?> GetCurrentUserAddressAsync(string email);
        Task<AddressDto?> UpdateCurrentUserAddressAsync(AddressDto request, string email);
    }
}
