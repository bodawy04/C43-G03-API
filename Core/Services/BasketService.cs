using Domain.Exceptions;
using Shared.DataTransferObjects.Basket;

namespace Services;

class BasketService(IBasketRepository basketRepository,IMapper mapper) : IBasketService
{
    public async Task<bool> DeleteAsync(string id) =>await basketRepository.DeleteAsync(id);

    public async Task<BasketDto> GetAsync(string id)
    {
        var basket = await basketRepository.GetBasketAsync(id)?? throw new BasketNotFoundException(id);
        return mapper.Map<BasketDto>(basket);
    }

    public async Task<BasketDto> UpdateAsync(BasketDto basket)
    {
        var customerBasket = mapper.Map<CustomerBasket>(basket);
        var updatedBasket = await basketRepository.UpdateBasketAsync(customerBasket) ?? throw new Exception("Cant update basket now");
return mapper.Map<BasketDto>(updatedBasket);    
    }
}
