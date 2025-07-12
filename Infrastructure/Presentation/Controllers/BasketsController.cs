using Microsoft.AspNetCore.Mvc;
using ServicesAbstractions;
using Shared.DataTransferObjects.Basket;

namespace Presentation.Controllers;

public class BasketsController(IServiceManager serviceManager):APIController
{
    // Get Basket by id
    [HttpGet]
    public async Task<ActionResult<BasketDto>> Get(string id)
    {
        var basket = await serviceManager.BasketService.GetAsync(id);
        return Ok(basket);
    }
    // Update Basket
    [HttpPost]
    public async Task<ActionResult<BasketDto>> Update(BasketDto basketDto)
    {
        var basket = await serviceManager.BasketService.UpdateAsync(basketDto);
        return Ok(basket);
    }
    // Delete Basket
    [HttpDelete("{id}")]
    public async Task<ActionResult<BasketDto>> Delete(string id)
    {
        await serviceManager.BasketService.DeleteAsync(id);
        return NoContent();//204
    }
}
