using Shared.DataTransferObjects.Products;

namespace ServicesAbstractions;

public interface IProductService
{
    //Task<IEnumerable<ProductResponse>> GetAllProductsAsync();
    Task<IEnumerable<ProductResponse>> GetAllProductsAsync(int? brandId, int? typeId,ProductSortingOptions options);
    Task<ProductResponse> GetProductByIdAsync(int id);
    Task<IEnumerable<BrandResponse>> GetBrandsAsync();
    Task<IEnumerable<TypeResponse>> GetTypesAsync();
    //Task<Product> CreateProductAsync(Product product);
    //Task<Product> UpdateProductAsync(Product product);
    //Task DeleteProductAsync(int id);
}
