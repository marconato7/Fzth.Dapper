using Bogus;
using Fzth.Dapper.Models;

namespace Fzth.Dapper.Data;

public static class FakeData
{
    private readonly static List<User> users = [];

    public static List<User> Users { get => users; }

    public static IEnumerable<User>? GenerateUsers(int count = 100)
    {
        var userFaker = new Faker<User>()
           .RuleFor(p => p.Name, f => f.Name.FullName())
           .RuleFor(p => p.Email, f => f.Internet.Email());

        var users = userFaker.Generate(count);

        return users;
    }
}
