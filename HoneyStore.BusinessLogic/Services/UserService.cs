using HoneyStore.BusinessLogic.Interfaces;
using HoneyStore.DataAccess.Identity;
using HoneyStore.DataAccess.UnitOfWork;

namespace HoneyStore.BusinessLogic.Services
{
    public class UserService : BaseService, IUserService
    {
        public UserService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
       
        public async Task<User> GetUserAsync(int id)
        {
            return await _uow.Users.GetAsync(id);
        }

        public async Task<ICollection<User>> GetAllUsersAsync()
        {
            return await _uow.Users.GetAllAsync();
        }

        public async Task<ICollection<Role>> GetUserRolesAsync(int userId)
        {
            return await _uow.Users.GetUserRolesAsync(userId);
        }
        
        public async Task AddUserAsync(User user)
        {
            await _uow.Users.AddAsync(user);

            await _uow.SaveAsync();
        }

        public async Task RemoveUserAsync(User user)
        {
            await _uow.Users.RemoveAsync(user);

            await _uow.SaveAsync();
        }

        public async Task UpdateUserAsync(int id, User user)
        {
            await _uow.Users.UpdateAsync(id, user);

            await _uow.SaveAsync();
        }
    }
}
