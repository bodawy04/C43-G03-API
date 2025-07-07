using Shared.Authentication;

namespace ServicesAbstractions;

public interface IAuthenticationService
{
    //[HttpPost]
    //Login(LoginRequest request{email,password})
    // =>UserResponse(string token, email, displayName)
    Task<UserResponse> LoginAsync(LoginRequest loginRequest);

    //[HttpPost]
    //Register(RegisterRequest request{email,password,confirmPassword,displayName})
    // =>UserResponse(string token, email, displayName)
    Task<UserResponse> RegisterAsync(RegisterRequest registerRequest);

    //[HttpGet]
    //CheckEmail(string email) => bool

    //TODO

    //[Authorize]
    //[HttpGet]
    //GetCurrentUserAddress() => AddressDto

    //[Authorize]
    //[HttpPut]
    //UpdateCurrentUserAddress(AddressDto) => AddressDto

    //[Authorize]
    //[HttpGet]
    //GetCurrentUser() => UserResponse(string token, string email, string displayName)

}
