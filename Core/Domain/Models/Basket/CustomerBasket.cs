namespace Domain.Models.Basket;

public class CustomerBasket
{
    public string Id { get; set; } // Created by client
    public ICollection<BasketItem> BasketItems { get; set; } = [];
}
