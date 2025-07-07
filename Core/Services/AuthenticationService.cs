namespace Services;

internal class AuthenticationService(UserManager<ApplicationUser> userManager,
    IOptions<JWTOptions> options,RoleManager<IdentityRole> roleManager) : IAuthenticationService
{
    public async Task<UserResponse> LoginAsync(LoginRequest loginRequest)
    {
        //Check if the user with email address exists
        var user = await userManager.FindByEmailAsync(loginRequest.Email) ??
            throw new UserNotFoundException(loginRequest.Email);

        //Check password for this user
        var isValid = await userManager.CheckPasswordAsync(user, loginRequest.Password);
        if (isValid) return new UserResponse(user.Email, user.DisplayName, await CreateTokenAsync(user));
        else throw new UnauthorizedException();
        //return response with token
    }

    public async Task<UserResponse> RegisterAsync(RegisterRequest registerRequest)
    {
        var user = new ApplicationUser
        {
            Email = registerRequest.Email,
            UserName = registerRequest.UserName,
            PhoneNumber = registerRequest.PhoneNumber,
            DisplayName = registerRequest.DisplayName
        };
        var result = await userManager.CreateAsync(user, registerRequest.Password);
        if (result.Succeeded)
            return new(registerRequest.Email, user.DisplayName, await CreateTokenAsync(user));
        var errors = result.Errors.Select(e => e.Description).ToList();

        throw new BadRequestException(errors);
    }
    private async Task<string> CreateTokenAsync(ApplicationUser user)
    {
        var jwt=options.Value;
        var claims = new List<Claim>
        {
            new(ClaimTypes.Email, user.Email!),
            new(ClaimTypes.Name, user.DisplayName),
        };
        var roles = await userManager.GetRolesAsync(user);
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.SecretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: jwt.Issuer,
            audience: jwt.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddDays(jwt.DurationInDays),
            signingCredentials: creds);
        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }
}
