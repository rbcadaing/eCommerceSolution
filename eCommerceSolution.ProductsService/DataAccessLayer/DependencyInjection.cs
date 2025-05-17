using DataAccessLayer.Context;
using eCommerce.DataAccessLayer.Repositories;
using eCommerce.DataAccessLayer.RepositoryContracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccessLayer;

public static class DependencyInjection
{

    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services,IConfiguration configuration)
    {
        string connectionStringTemplate = configuration.GetConnectionString("DefaultConnection")!;
        services.AddDbContext<ApplicationDbContext>(options => options.UseMySQL(connectionStringTemplate));
        services.AddScoped<IProductsRepository, ProductsRepository>();
        return services;
    }
}
