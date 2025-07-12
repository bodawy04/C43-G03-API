namespace Domain.Models.OrderModels;
public class Order : BaseEntity<Guid>
{
    public Order()
    {
        
    }
    public Order(string userEmail, IEnumerable<OrderItem> items, OrderAddress address,
        DeliveryMethod deliveryMethod, decimal subTotal)
    {
        UserEmail = userEmail;
        Items = items;
        Address = address;
        DeliveryMethod = deliveryMethod;
        //PaymentIntentId = paymentIntentId;
        SubTotal = subTotal;
    }

    public string UserEmail { get; set; } = default!;
    public DateTimeOffset OrderTime { get; set; } = DateTimeOffset.Now;
    public IEnumerable<OrderItem> Items { get; set; } = [];
    public OrderAddress Address { get; set; } = default!;
    public DeliveryMethod DeliveryMethod { get; set; } = default!;
    public int DeliveryMethodID { get; set; }
    public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;
    public string PaymentIntentId { get; set; } = string.Empty;
    public decimal SubTotal { get; set; }
}
