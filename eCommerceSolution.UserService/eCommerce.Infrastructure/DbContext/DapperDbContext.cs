using System.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace eCommerce.Infrastructure.DbContext;

public class DapperDbContext
{
    private readonly IConfiguration _configuration;
    private readonly IDbConnection _connection;

    public DapperDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
        string? connectionsString = _configuration.GetConnectionString("PostgresConnection")!;
        //connectionsString.Replace("$POSTGRES_HOST", Environment.GetEnvironmentVariable("POSTGRES_HOST"))
        //    .Replace("$POSTGRES_PASSWORD", Environment.GetEnvironmentVariable("POSTGRES_PASSWORD"));

        _connection = new NpgsqlConnection(connectionsString);
    }

    public IDbConnection DBConnection => _connection;
}
