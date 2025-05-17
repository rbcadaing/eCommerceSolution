using DataAccessLayer.Repositories;
using DataAccessLayer.RepositoryContracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace DataAccessLayer;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionStringTemplate = configuration.GetConnectionString("MongoDB")!;
        services.AddSingleton<IMongoClient>(sp =>
        {
            var mongoClient = new MongoClient(connectionStringTemplate);
            return mongoClient;
        }); 

        services.AddScoped<IMongoDatabase>(sp =>
        {
            var client = sp.GetRequiredService<IMongoClient>();
            return client.GetDatabase("OrdersDatabase");
        });

        services.AddScoped<IOrdersRepository, OrdersRepository>();
        return services;
    }
}
