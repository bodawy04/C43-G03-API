using Domain.Exceptions;
using Shared.ErrorModels;
using System.Net;
using System.Text.Json;

namespace C43_G03_API.Middlewares;

public class CustomExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    //private readonly Logger<CustomExceptionHandlerMiddleware> _logger;

    public CustomExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
        //_logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
            await HandleNotFoundEndPointAsync(context);
        }
        catch (Exception ex)
        {
            //_logger.LogError(ex, "Something went wrong");
            await HandleExceptionAsync(context, ex);
            //Return Response as JSON
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Response.ContentType = "application/json";
        var response = new ErrorDetails
        {
            ErrorMessage = ex.Message
        };
        response.StatusCode = ex switch
        {
            NotFoundException => StatusCodes.Status404NotFound,
            UnauthorizedException => StatusCodes.Status401Unauthorized,
            BadRequestException badRequestException=> GetValidationErrors(badRequestException,response),
            _ => StatusCodes.Status500InternalServerError
        };
        //var jsonResult=JsonSerializer.Serialize(response);
        //await context.Response.WriteAsync(jsonResult);
        /// OR
        context.Response.StatusCode = response.StatusCode;
        await context.Response.WriteAsJsonAsync(response);

        //Set Status Code
        //Set Content Type
        //Response Object
    }

    private static int GetValidationErrors(BadRequestException badRequestException, ErrorDetails response)
    {
        response.Errors=badRequestException.Errors;
        return StatusCodes.Status400BadRequest;
    }

    private static async Task HandleNotFoundEndPointAsync(HttpContext context)
    {
        if (context.Response.StatusCode == (int)HttpStatusCode.NotFound)
        {
            context.Response.ContentType = "application/json";
            var response = new ErrorDetails
            {
                ErrorMessage = $"End Point {context.Request.Path} Not Found",
                StatusCode = (int)HttpStatusCode.NotFound
            };
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
public static class CustomExceptionHandlerMiddlewareExtensions
{
    public static IApplicationBuilder UseCustomExceptionMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<CustomExceptionHandlerMiddleware>();
        return app;
    }
    
}
