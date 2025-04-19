using Domain.Exceptions;
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
    //public async Task<IEnumerable<ProductResponse>> GetAllProductsAsync(int? brandId, int? typeId,ProductSortingOptions options)
    //{
    //    var specifications = new ProductWithBrandAndTypeSpecifications(brandId,typeId,options);
    //    var product = await unitOfWork.GetRepository<Product, int>().GetAllAsync(specifications);
    //    return mapper.Map<IEnumerable<Product>, IEnumerable<ProductResponse>>(product);
    //}
    //public async Task<IEnumerable<ProductResponse>> GetAllProductsAsync(ProductQueryParameters queryParameters)
    //{
    //    var specifications = new ProductWithBrandAndTypeSpecifications(queryParameters);
    //    var product = await unitOfWork.GetRepository<Product, int>().GetAllAsync(specifications);
    //    return mapper.Map<IEnumerable<Product>, IEnumerable<ProductResponse>>(product);
    //}
    public async Task<PaginatedResponse<ProductResponse>> GetAllProductsAsync(ProductQueryParameters queryParameters)
    {
        var specifications = new ProductWithBrandAndTypeSpecifications(queryParameters);
        var product = await unitOfWork.GetRepository<Product, int>().GetAllAsync(specifications);
        var data = mapper.Map<IEnumerable<Product>, IEnumerable<ProductResponse>>(product);
        var pageCount = data.Count();
        var totalCount = await unitOfWork.GetRepository<Product, int>()
            .CountAsync(new ProductsCountSpecifications(queryParameters));
        return new(queryParameters.PageSize, pageCount, totalCount, data);
    }
    public async Task<ProductResponse> GetProductByIdAsync(int id)
    {
        var specifications = new ProductWithBrandAndTypeSpecifications(id);
        var product = await unitOfWork.GetRepository<Product, int>().GetAsync(specifications) 
            ?? throw new ProductNotFoundException(id);
        return mapper.Map<Product, ProductResponse>(product);
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
