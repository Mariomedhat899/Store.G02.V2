using Shared.Dtos.Orders;

namespace Services.Abstractions.Orders
{
    public interface IOrderService
    {
        Task<OrderResponse?> CreateOrderAsync(OrderRequest request, string userEmail);

        Task<IEnumerable<DeliveyMethodResponse>> GetAllDeliveyMethodsAsync();

        Task<OrderResponse?> GetOrderByIdForSpecificUserAsync(Guid id, string userEmail);
        Task<IEnumerable<OrderResponse>> GetOrdesForSpecificUserAsync(string userEmail);
    }
}
