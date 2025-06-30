using Fzth.Dapper.Models;

namespace Fzth.Dapper.Data;

public interface IUserRepository
{
    Task<IEnumerable<User>?> GetAllAsync();
    Task<User?> GetByIdAsync(int id);
    Task<int> InsertAsync(User user);
    Task<int> UpdateAsync(User user);
    Task<object> DeleteByIdAsync(int customerId);
}
