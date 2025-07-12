using Shared.Orders;

namespace ServicesAbstractions;

public interface IOrderService
{
    Task<OrderResponse> CreateAsync(OrderRequest request, string email);
    Task<OrderResponse> GetAsync(Guid Id);
    Task<IEnumerable<OrderResponse>> GetAllAsync(string email);
    Task<IEnumerable<DeliveryMethodResponse>> GetDeliveryMethodsAsync();
}
