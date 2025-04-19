using Microsoft.AspNetCore.Mvc;
using Shared.ErrorModels;

namespace C43_G03_API.Factories
{
    public static class APIResponseFactory
    {
        public static IActionResult GenerateAPIValidationResponse(ActionContext context)
        {
            //Get Entries in model state that have validation errors
            var errors = context.ModelState
                .Where(m => m.Value.Errors.Any())
                .Select(m => new ValidationError
                {
                    Field = m.Key,
                    Errors = m.Value.Errors.Select(e => e.ErrorMessage)
                });
            var response = new ValidationErrorResponse { ValidationErrors = errors };
            return new BadRequestObjectResult(response);
        }
    }
}
