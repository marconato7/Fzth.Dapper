using System.Data.SQLite;
using Dapper;
using Fzth.Dapper.Models;

namespace Fzth.Dapper.Data;

public class UserRepository : IUserRepository
{
    private readonly SQLiteConnection _connection;

    public UserRepository(SQLiteConnection connection)
    {
        _connection = connection ?? throw new ArgumentNullException(nameof(connection));
    }

    public async Task<object> DeleteByIdAsync(int customerId)
    {
        const string sql = "DELETE FROM Users WHERE Id = @customerId";

        var result = await _connection.ExecuteAsync(sql, new { customerId });

        return new
        {
            customerId,
            affectedRows = result
        };
    }

    public async Task<IEnumerable<User>?> GetAllAsync()
    {
        const string sql = "SELECT Id, Name, Email FROM Users;";
        var users = await _connection.QueryAsync<User>(sql);
        return users;
    }

    public async Task<User?> GetByIdAsync(int customerId)
    {
        const string sql = "SELECT Id, Name, Email FROM Users WHERE Id = @customerId";
        var user = await _connection.QueryFirstOrDefaultAsync<User>(sql, new { customerId });
        return user;
    }

    public Task<int> InsertAsync(User user)
    {
        throw new NotImplementedException("InsertAsync method is not implemented yet.");
    }

    public Task<int> UpdateAsync(User user)
    {
        throw new NotImplementedException("InsertAsync method is not implemented yet.");
    }
}
