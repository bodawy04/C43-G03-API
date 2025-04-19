namespace Shared.ErrorModels;

public class ValidationErrorResponse
{
    public int StatusCode { get; set; }
    public string ErrorMessage { get; set; } = "Validation Failed";
    public IEnumerable<ValidationError> ValidationErrors { get; set; } = [];
}
