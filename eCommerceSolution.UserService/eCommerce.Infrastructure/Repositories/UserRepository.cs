using Dapper;
using eCommerce.Core.Dto;
using eCommerce.Core.Entities;
using eCommerce.Core.RepositoryContracts;
using eCommerce.Infrastructure.DbContext;
using Npgsql.Replication.PgOutput.Messages;

namespace eCommerce.Infrastructure.Repositories;

internal class UserRepository : IUserRepository
{
    private readonly DapperDbContext _dbContext;

    public UserRepository(DapperDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<ApplicationUser?> AddUser(ApplicationUser user)
    {
        user.UserID = Guid.NewGuid();

        //SQL Query to insert data into the "Users" table
        string query = "INSERT INTO public.\"Users\"(\"UserID\",\"Email\",\"PersonName\",\"Gender\",\"Password\")" +
            "VALUES(@USerID,@Email,@PersonName,@Gender,@Password)";

        int rowCountAffected = await _dbContext.DBConnection.ExecuteAsync(query, user);
        if (rowCountAffected > 0)
        {
            return user!;
        }
        else
        {
            return null;
        }
    }

    public async Task<ApplicationUser?> GetUserByEmailAndPassword(string? email, string? password)
    {

        string query = "SELECT * FROM public.\"Users\" WHERE \"Email\"=@Email AND \"Password\"= @Password";
        var parameters = new { Email = email, Password = password };
        var user = await _dbContext.DBConnection.QueryFirstOrDefaultAsync<ApplicationUser>(query, parameters);

        return user;
    }

    public async Task<ApplicationUser> GetUserByUserID(Guid? userId)
    {
        string query = "SELECT * FROM public.\"Users\" WHERE \"UserID\"=@UserId";
        var parameters = new { UserID = userId };
        var user = await _dbContext.DBConnection.QueryFirstOrDefaultAsync<ApplicationUser>(query, parameters);

        return user!;
    }
}
