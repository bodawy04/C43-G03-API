namespace Shared.Authentication;

public class JWTOptions
{
    public string SecretKey { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public int DurationInDays{ get; set; } = 30;
    //public bool ValidateIssuerSigningKey { get; set; } = true;
    //public bool ValidateIssuer { get; set; } = true;
    //public bool ValidateAudience { get; set; } = true;
    //public bool ValidateLifetime { get; set; } = true;
}
