using HoneyStore.DataAccess.Identity;

namespace HoneyStore.DataAccess.Interfaces
{
    public interface IUserRepository: IGenericRepository<User>
    {
        Task<ICollection<Role>> GetUserRolesAsync(int userId);
    }
}
