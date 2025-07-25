﻿namespace Services;

public class ServiceManager(IMapper mapper, IUnitOfWork unitOfWork,IBasketRepository basketRepository,
    UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager, IOptions<JWTOptions> options)
    : IServiceManager
{
    private readonly Lazy<IProductService> _lazyProductService =
        new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));
    private readonly Lazy<IBasketService> _lazyBasketService =
        new Lazy<IBasketService>(() => new BasketService(basketRepository, mapper));
    private readonly Lazy<IAuthenticationService> _lazyAuthenticationService =
        new(() => new AuthenticationService(userManager,options,roleManager));
    public IProductService ProductService => _lazyProductService.Value;

    public IBasketService BasketService => _lazyBasketService.Value;

    public IAuthenticationService AuthenticationService => _lazyAuthenticationService.Value;
}
