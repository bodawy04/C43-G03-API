using Domain.Models.OrderModels;
using Shared.Orders;
using DeliveryMethod = Domain.Models.OrderModels.DeliveryMethod;

namespace Services;

internal class OrderService(IMapper mapper, IUnitOfWork _unitOfWork, IBasketRepository _basketRepository) : IOrderService
{
    public async Task<OrderResponse> CreateAsync(OrderRequest request, string email)
    {
        var basket = await _basketRepository.GetBasketAsync(request.BasketId) ??
            throw new BasketNotFoundException(request.BasketId);

        List<OrderItem> items = [];
        var productRepo = _unitOfWork.GetRepository<Product, int>();

        foreach (var item in basket.BasketItems)
        {
            var product = await productRepo.GetAsync(item.Id) ?? throw new ProductNotFoundException(item.Id);
            items.Add(CreateOrderItem(product, item));
            item.Price = product.Price;
        }
        var address = mapper.Map<OrderAddress>(request.Address);
        var method = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetAsync(request.DeliveryMethodId)
            ??throw new DeliveryMethodNotFoundException(request.DeliveryMethodId);
        var subTotal = items.Sum(x=>x.Quantity*x.Price);
        var order = new Order(email, items, address, method, subTotal);
        _unitOfWork.GetRepository<Order, Guid>().Add(order);
        await _unitOfWork.SaveChangesAsync();
        return mapper.Map<OrderResponse>(order);
    }

    private static OrderItem CreateOrderItem(Product product, BasketItem item)
    => new(new(product.Id, product.Name, product.PictureUrl), product.Price, item.Quantity);

    public async Task<IEnumerable<OrderResponse>> GetAllAsync(string email)
    {
        var orders = await _unitOfWork.GetRepository<Order, Guid>().GetAllAsync(new OrderSpecifications(email));
        return mapper.Map<IEnumerable<OrderResponse>>(orders);
    }

    public async Task<OrderResponse> GetAsync(Guid Id)
    {
        var order = await _unitOfWork.GetRepository<Order, Guid>().GetAsync(new OrderSpecifications(Id));
        return mapper.Map<OrderResponse>(order);
    }

    public async Task<IEnumerable<DeliveryMethodResponse>> GetDeliveryMethodsAsync()
    {
        var deliveryMethods = await _unitOfWork.GetRepository<DeliveryMethod,int>().GetAllAsync();
        return mapper.Map<IEnumerable<DeliveryMethodResponse>>(deliveryMethods);
    }
}
