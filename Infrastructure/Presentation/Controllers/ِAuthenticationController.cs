using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicesAbstractions;
using Shared.Authentication;
using System.Security.Claims;

namespace Presentation.Controllers;

public class AuthenticationController(IServiceManager serviceManager) : APIController
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
    [HttpGet("CheckEmail")]
    public async Task<ActionResult<bool>> CheckEmail(string email)
        => Ok(await serviceManager.AuthenticationService.CheckEmailAsync(email));

    //GetUserAddress() => AddressDto
    [Authorize]
    [HttpGet("Address")]
    public async Task<ActionResult<AddressDto>> GetAddress()
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        return Ok(await serviceManager.AuthenticationService.GetUserAddressAsync(email!));
    }
    //UpdateCurrentUserAddress(AddressDto) => AddressDto
    [Authorize]
    [HttpPut("Address")]
    public async Task<ActionResult<AddressDto>> UpdateAddress(AddressDto addressDto)
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        return Ok(await serviceManager.AuthenticationService.UpdateUserAddressAsync(addressDto, email!));
    }

    //GetCurrentUser() => UserResponse(string token, email, displayName)
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<UserResponse>> GetCurrentUser(string email)
        => Ok(await serviceManager.AuthenticationService.GetUserByEmail(email));

    //All with [Authorize] except Login and Register
}
