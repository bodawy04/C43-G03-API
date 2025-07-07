namespace ServicesAbstractions;

public interface IServiceManager
{
    IProductService ProductService { get; }
    IBasketService BasketService { get; }
    IAuthenticationService AuthenticationService { get; }
}
