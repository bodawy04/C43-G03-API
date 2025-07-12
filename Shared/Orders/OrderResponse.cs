using Shared.Authentication;

namespace Shared.Orders;

public record OrderResponse
{
    public Guid Id { get; set; }
    public string UserEmail { get; set; } = default!;
    public DateTimeOffset OrderTime { get; set; } = DateTimeOffset.Now;
    public IEnumerable<OrderItemDto> Items { get; set; } = [];
    public AddressDto Address { get; set; } = default!;
    public string DeliveryMethod { get; set; } = default!;
    public string PaymentStatus { get; set; } 
    public string PaymentIntentId { get; set; } = string.Empty;
    public decimal Total { get; set; }
    public decimal Price { get; set; }
}

public record OrderItemDto
{
    public string PictureUrl { get; set; }
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public int Quantity{ get; set; }
}