using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicesAbstractions;
using Shared.DataTransferObjects.Products;
using System.Security.Claims;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController(IServiceManager serviceManager) : ControllerBase
{
    //[HttpGet]
    //public async Task<ActionResult<IEnumerable<ProductResponse>>> GetAllProducts()
    //{
    //    var products = await serviceManager.ProductService.GetAllProductsAsync();
    //    return Ok(products);
    //}

    //[HttpGet]
    //public async Task<ActionResult<IEnumerable<ProductResponse>>> GetAllProducts(int? brandId, int? typeId, ProductSortingOptions sort)
    //{
    //    var products = await serviceManager.ProductService.GetAllProductsAsync(brandId,typeId,sort);
    //    return Ok(products);
    //}
    //[HttpGet]
    //public async Task<ActionResult<IEnumerable<ProductResponse>>> GetAllProducts([FromQuery]ProductQueryParameters queryParameters)
    //{
    //    var products = await serviceManager.ProductService.GetAllProductsAsync(queryParameters);
    //    return Ok(products);
    //}
    [HttpGet]
    public async Task<ActionResult<PaginatedResponse<ProductResponse>>> GetAllProducts([FromQuery] ProductQueryParameters queryParameters)
    {
        throw new Exception("Test");
        var products = await serviceManager.ProductService.GetAllProductsAsync(queryParameters);
        return Ok(products);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductResponse>> GetProduct(int id) //Get baseUrl/api/Products/{id}
    {
        var product = await serviceManager.ProductService.GetProductByIdAsync(id);
        return Ok(product);
    }
    [Authorize(Roles = "Admin")]
    [HttpGet("brands")]
    public async Task<ActionResult<IEnumerable<BrandResponse>>> GetBrands() //Get baseUrl/api/Products/brands
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var brands = await serviceManager.ProductService.GetBrandsAsync();
        return Ok(brands);
    }
    [HttpGet("types")]
    public async Task<ActionResult<IEnumerable<TypeResponse>>> GetTypes() //Get baseUrl/api/Products/types
    {
        var types = await serviceManager.ProductService.GetTypesAsync();
        return Ok(types);
    }
}
