namespace Fzth.Dapper.Models;

public sealed class User
{
    public int Id       { get; private set; }
    public string Name  { get; private set; }
    public string Email { get; private set; }

    public User(string name, string email)
    {
        Name  = name;
        Email = email;
    }

    private User() {} 
}
