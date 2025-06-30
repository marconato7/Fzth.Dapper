using System.Data.SQLite;
using Dapper;
using Fzth.Dapper.Models;
using Z.Dapper.Plus;

namespace Fzth.Dapper.Data;

public static class DatabaseUtils
{
    public static async Task MigrateDatabaseAsync()
    {
        using var connection = new SQLiteConnection(Config.ConnectionString);

        await connection.OpenAsync();

        const string sql =
        """
            CREATE TABLE IF NOT EXISTS Users
            (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT NOT NULL,
                Email TEXT NOT NULL UNIQUE
            );
        """;

        await connection.ExecuteAsync(sql);
    }

    public static async Task SeedDatabaseAsync()
    {
        await SeedUsers();
    }

    private static async Task SeedUsers()
    {
        DapperPlusManager.Entity<User>().Table("Users");

        using var connection = new SQLiteConnection(Config.ConnectionString);

        await connection.OpenAsync();

        const string sql = "SELECT COUNT(*) FROM Users";

        var count = await connection.ExecuteScalarAsync<int>(sql);
        if (count == 0)
        {
            await connection.BulkInsertAsync(FakeData.GenerateUsers());
        }
    }
}
