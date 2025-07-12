namespace Domain.Models.OrderModels;

public class OrderItem : BaseEntity<Guid>
{
    public OrderItem()
    {
        
    }
    public OrderItem(ProductInOrderItem productInOrderItem, decimal price, int quantity)
    {
        ProductInOrderItem = productInOrderItem;
        Price = price;
        Quantity = quantity;
    }

    public ProductInOrderItem ProductInOrderItem { get; set; } = default!;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
