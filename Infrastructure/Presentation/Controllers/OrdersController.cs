using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicesAbstractions;
using Shared.Orders;

namespace Presentation.Controllers;
[Authorize]
public class OrdersController(IServiceManager service) : APIController
{
    /// Create (address, basketId, deliveryMethodID) => OrderResponse
    [HttpPost]
    public async Task<ActionResult<OrderResponse>> Create(OrderRequest request)
    {
        return Ok(await service.OrderService.CreateAsync(request, GetEmailFromToken()));
    }
    /// GetAll
    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderResponse>>> GetAll(OrderRequest request)
    {
        return Ok(await service.OrderService.GetAllAsync(GetEmailFromToken()));
    }
    /// Get
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<OrderResponse>> Get(Guid id)
    {
        return Ok(await service.OrderService.GetAsync(id));
    }
    /// GetDeliveryMethods()
     [HttpGet("deliveryMethods"),AllowAnonymous]
    public async Task<ActionResult<DeliveryMethodResponse>> GetDeliveryMethods(Guid id)
    {
        return Ok(await service.OrderService.GetDeliveryMethodsAsync());
    }

}
