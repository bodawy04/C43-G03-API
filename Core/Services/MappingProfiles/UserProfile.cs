namespace Services.MappingProfiles;

internal class UserProfile:Profile
{
    public UserProfile()
    {
        CreateMap<AddressDto,Address>().ReverseMap();
    }
}
