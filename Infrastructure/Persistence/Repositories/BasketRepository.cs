
using StackExchange.Redis;

namespace Persistence.Repositories;

class BasketRepository(IConnectionMultiplexer connectionMultiplexer) : IBasketRepository
{
    private readonly IDatabase _database = connectionMultiplexer.GetDatabase();
    public async Task<bool> DeleteAsync(string Id) => await _database.KeyDeleteAsync(Id);

    public async Task<CustomerBasket?> GetBasketAsync(string Id)
    {
        //Get Object from DB
        //Deserialization
        //Return

        var basket = await _database.StringGetAsync(Id);
        if (basket.IsNullOrEmpty) return null;
        return JsonSerializer.Deserialize<CustomerBasket>(basket!);
        //return JsonSerializer.Deserialize<CustomerBasket>(basket);
    }

    //Used For Create and Update
    public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket, TimeSpan? timeToLive = null)
    {
        var jsonBasket = JsonSerializer.Serialize(basket);
        var isCreatedOrUpdated=await _database.StringSetAsync(basket.Id, jsonBasket,timeToLive??TimeSpan.FromDays(30));
        return isCreatedOrUpdated? await GetBasketAsync(basket.Id): null;
    }
}
