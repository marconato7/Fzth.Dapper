using System.Data.SQLite;
using Fzth.Dapper.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped((sp) => new SQLiteConnection(Config.ConnectionString));

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    await DatabaseUtils.MigrateDatabaseAsync();
    await DatabaseUtils.SeedDatabaseAsync();
}

app.UseHttpsRedirection();

app.MapGet("/api/users", async (IUserRepository userRepository) =>
{
    var users = await userRepository.GetAllAsync();

    return Results.Ok(users);
});

app.MapGet("/api/users/{id:int}", async (int id, IUserRepository userRepository) =>
{
    var userOrNull = await userRepository.GetByIdAsync(id);
    if (userOrNull is null)
    {
        return Results.NotFound(id);
    }

    return Results.Ok(userOrNull);
});

app.MapDelete("/api/users/{id:int}", async (int id, IUserRepository userRepository) =>
{
    var userOrNull = await userRepository.DeleteByIdAsync(id);
    if (userOrNull is null)
    {
        return Results.NotFound(id);
    }

    return Results.Ok(userOrNull);
});

app.Run();
