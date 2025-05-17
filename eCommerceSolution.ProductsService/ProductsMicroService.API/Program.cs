using BusinessLogicLayer;
using DataAccessLayer;
using FluentValidation.AspNetCore;
using ProductsMicroService.API.APIEndpoints;
using ProductsMicroService.API.MiddleWare;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDataAccessLayer(builder.Configuration);
builder.Services.AddBusinessLogicLayer();

builder.Services.AddControllers();

//Fluent Validations
//Fluent Validations only works on controller type API
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Products Microservice API", Version = "v1" });
});

//Cors
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:4200")
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseExceptionHandlingMiddleware();
app.UseRouting();

//Cors
app.UseCors();


app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Products Microservice API V1");
    c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
});

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// Map minimal API endpoints from separate class
app.MapProductAPIEndpoints();

app.Run();
