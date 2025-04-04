using Eformerapp.Data.Entities;
using System.Threading.Tasks;

namespace Eformerapp.Data.Repository.Interface
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<IEnumerable<User>> GetUsersByNameAsync(string name);
        Task<User> CreateUserAsync(User user);
        Task<User> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(int id);
        Task<User> GetByMobileNumberAsync(string mobileNumber);
    }
}
