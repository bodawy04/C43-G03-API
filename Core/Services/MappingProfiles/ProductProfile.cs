using Domain.Models.Products;
using Microsoft.Extensions.Configuration;

namespace Services.MappingProfiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductResponse>()
            .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.ProductBrand.Name))
            .ForMember(dest => dest.TypeName, opt => opt.MapFrom(src => src.ProductType.Name))
            .ForMember(dest=>dest.PictureUrl,opt=>opt.MapFrom<PictureUrlResolver>());
        CreateMap<ProductBrand, BrandResponse>();
        CreateMap<ProductType, TypeResponse>();
    }
}
internal class PictureUrlResolver(IConfiguration configuration) : IValueResolver<Product, ProductResponse, string>
{
    public string Resolve(Product source, ProductResponse destination, string destMember, ResolutionContext context)
    {
        if (!string.IsNullOrWhiteSpace(source.PictureUrl))
        {
            return $"{configuration["BaseUrl"]}{source.PictureUrl}";
        }
        return "";
    }
}