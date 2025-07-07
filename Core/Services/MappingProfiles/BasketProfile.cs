namespace Services.MappingProfiles;

class BasketProfile:Profile
{
    public BasketProfile()
    {
        CreateMap<CustomerBasket, BasketDto>().ReverseMap();
        CreateMap<BasketItem, BasketItemDto>().ReverseMap();
    }
}
