using Microsoft.Extensions.Options;
using Services.Specifications;

namespace Services;

public class ProductService(IUnitOfWork unitOfWork, IMapper mapper) : IProductService
{
    //public async Task<IEnumerable<ProductResponse>> GetAllProductsAsync()
    //{
    //    var specifications = new ProductWithBrandAndTypeSpecifications();
    //    var product = await unitOfWork.GetRepository<Product, int>().GetAllAsync(specifications);
    //    return mapper.Map<IEnumerable<Product>, IEnumerable<ProductResponse>>(product);
    //}
    public async Task<IEnumerable<ProductResponse>> GetAllProductsAsync(int? brandId, int? typeId,ProductSortingOptions options)
    {
        var specifications = new ProductWithBrandAndTypeSpecifications(brandId,typeId,options);
        var product = await unitOfWork.GetRepository<Product, int>().GetAllAsync(specifications);
        return mapper.Map<IEnumerable<Product>, IEnumerable<ProductResponse>>(product);
    }
    public async Task<ProductResponse> GetProductByIdAsync(int id)
    {
        var specifications = new ProductWithBrandAndTypeSpecifications(id);
        var product = await unitOfWork.GetRepository<Product, int>().GetAsync(specifications);
        return mapper.Map<Product,ProductResponse>(product);
    }

    public async Task<IEnumerable<BrandResponse>> GetBrandsAsync()
    {
        var brands = unitOfWork.GetRepository<ProductBrand, int>();
        var result = await brands.GetAllAsync();
        return mapper.Map<IEnumerable<ProductBrand>, IEnumerable<BrandResponse>>(result);
    }


    public async Task<IEnumerable<TypeResponse>> GetTypesAsync()
    {
        var types = unitOfWork.GetRepository<ProductType, int>();
        var result = await types.GetAllAsync();
        return mapper.Map<IEnumerable<ProductType>, IEnumerable<TypeResponse>>(result);
    }
}
