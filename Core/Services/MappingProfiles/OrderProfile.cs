using Domain.Models.OrderModels;
using Shared.Orders;

namespace Services.MappingProfiles;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<OrderAddress, AddressDto>().ReverseMap();
        CreateMap<OrderItem, OrderItemDto>()
            .ForMember(d => d.ProductName,
            o => o.MapFrom(s => s.ProductInOrderItem.ProductName))
            .ForMember(d => d.PictureUrl, o => o.MapFrom<OrderItemPictureUrlResolver>());
        CreateMap<Order, OrderResponse>()
            .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
            .ForMember(d => d.Total, o => o.MapFrom(s => s.SubTotal + s.DeliveryMethod.Price));
        CreateMap<Domain.Models.OrderModels.DeliveryMethod, DeliveryMethodResponse>();
    }
}
internal class OrderItemPictureUrlResolver(IConfiguration configuration)
    : IValueResolver<OrderItem, OrderItemDto, string>
{
    public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
    {
        if (!string.IsNullOrWhiteSpace(source.ProductInOrderItem.PictureUrl))
        {
            return $"{configuration["BaseUrl"]}{source.ProductInOrderItem.PictureUrl}";
        }
        return "";
    }
}