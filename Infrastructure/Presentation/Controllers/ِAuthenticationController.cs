using Microsoft.AspNetCore.Mvc;
using ServicesAbstractions;
using Shared.Authentication;

namespace Presentation.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthenticationController(IServiceManager serviceManager): ControllerBase
{
    //[HttpPost]
    //Login(LoginRequest request{email,password})
    // =>UserResponse(string token, email, displayName)

    [HttpPost("login")]
    public async Task<ActionResult<UserResponse>> Login(LoginRequest request) 
        => Ok(await serviceManager.AuthenticationService.LoginAsync(request));

    //[HttpPost]
    //Register(RegisterRequest request{email,password,confirmPassword,displayName})
    // =>UserResponse(string token, email, displayName)

    [HttpPost("register")]
    public async Task<ActionResult<UserResponse>> Register(RegisterRequest request)
    => Ok(await serviceManager.AuthenticationService.RegisterAsync(request));

    //CheckEmailAddress

    //GetUserAddress() => AddressDto

    //UpdateCurrentUserAddress(AddressDto) => AddressDto

    //GetCurrentUser() => UserResponse(string token, email, displayName)

    //All with [Authorize] except Login and Register
}
