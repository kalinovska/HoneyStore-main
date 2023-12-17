using HoneyStore.DataAccess.Identity;

namespace HoneyStore.BusinessLogic.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserAsync(int id);
        Task<ICollection<User>> GetAllUsersAsync();

        Task<ICollection<Role>> GetUserRolesAsync(int userId);
        
        Task AddUserAsync(User user);

        Task RemoveUserAsync(User user);

        Task UpdateUserAsync(int id, User user);
    }
}
