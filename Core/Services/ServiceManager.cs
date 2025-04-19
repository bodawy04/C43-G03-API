namespace Services;

public class ServiceManager(IMapper mapper, IUnitOfWork unitOfWork) : IServiceManager
{
    private readonly Lazy<IProductService> _lazyProductService =
        new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));
    public IProductService ProductService => _lazyProductService.Value;
}
