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

}
