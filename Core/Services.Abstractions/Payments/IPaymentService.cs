using Shared.Dtos.Baskets;

namespace Services.Abstractions.Payments
{
    public interface IPaymentService
    {
        Task<BasketDto> CreatePaymentIntent(string BasketId);
    }
}
