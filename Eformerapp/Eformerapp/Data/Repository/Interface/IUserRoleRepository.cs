using Eformerapp.Data.Entities;

namespace Eformerapp.Data.Repository.Interface
{
    public interface IUserRoleRepository
    {
        Task<IEnumerable<UserRole>> GetAllAsync();
        Task<UserRole> GetByIdAsync(int id);
        Task<IEnumerable<UserRole>> GetByNameAsync(string name);
        Task<UserRole> CreateAsync(UserRole userRole);
        Task<UserRole> UpdateAsync(UserRole userRole);
    }
}
