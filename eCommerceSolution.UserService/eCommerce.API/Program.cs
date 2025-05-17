using eCommerce.API.Middlewares;
using eCommerce.Core;
using eCommerce.Core.Mappers;
using eCommerce.Infrastructure;
using FluentValidation.AspNetCore;
using System.Text.Json.Serialization;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddInfrastrucre();
        builder.Services.AddCore();

        builder.Services.AddControllers().AddJsonOptions(options =>
        {
            // fix model binding of request string parameter to enum type eg: GenderOption.Female as "gender":"female" in the request body 
            options.JsonSerializerOptions.Converters.Add
            (new JsonStringEnumConverter());
        });

        builder.Services.AddAutoMapper(typeof(ApplicationUserMappingProfile).Assembly);

        //Fluent Validation

        builder.Services.AddFluentValidationAutoValidation();

        // Add API endpoint explorer
        builder.Services.AddEndpointsApiExplorer();

        // Add Swagger generation service to create swagger specification
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
            {
                Title = "eCommerce API",
                Version = "v1"
            });
        });

        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder
                .WithOrigins("http://localhost:4200")
                .AllowAnyMethod()
                .AllowAnyHeader();
            });
        });

        var app = builder.Build();

        //Global Exception Handling
        app.UseExceptionHandlingMiddleware();

        //Routing
        app.UseRouting();

        app.UseSwagger(); // adds endpoint that can serve the generated swagger specification
        app.UseSwaggerUI();

        app.UseCors();

        //Auth
        app.UseAuthentication();
        app.UseAuthorization();

        //Controller routes
        app.MapControllers();

        app.Run();
    }
}