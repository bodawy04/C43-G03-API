using C43_G03_API.Factories;
using C43_G03_API.Middlewares;
using Domain.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data;
using Persistence.Repositories;
using Services;
using ServicesAbstractions;
using Shared.ErrorModels;

namespace C43_G03_API;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddApplicationServices(builder.Configuration);
        builder.Services.AddInfrastructureServices(builder.Configuration);
        builder.Services.AddSwaggerServices();
        builder.Services.AddWebApplicationServices(builder.Configuration);
        
        

        var app = builder.Build();
        //await InitializeDbAsync(app);
        await app.InitializeDataBaseAsync();
        app.UseCustomExceptionMiddleware();
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseStaticFiles();
        app.UseHttpsRedirection();

        app.UseAuthorization();
        app.UseAuthentication();


        app.MapControllers();

        app.Run();
    }
}
