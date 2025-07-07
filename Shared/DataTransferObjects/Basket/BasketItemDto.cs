using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects.Basket;

public record BasketItemDto
{
    public int Id { get; init; }
    public string ProductName { get; init; } = default!;
    public string PictureUrl { get; init; } = default!;
    [Range(1,double.MaxValue)]
    public decimal? Price { get; init; }
    public int Quantity { get; init; }
}
