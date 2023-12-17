using HoneyStore.DataAccess.Context;
using HoneyStore.DataAccess.Identity;
using HoneyStore.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HoneyStore.DataAccess.Repositories
{
    public class UserRepository: BaseRepository<User>, IUserRepository
    {
        public UserRepository(StoreDbContext context): base(context)
        {
            
        }

        public async Task<ICollection<Role>> GetUserRolesAsync(int userId)
        {
            return await _context.UserRoles
                .Where(ur => ur.UserId == userId)
                .Select(ur => _context.Roles.Single(r => r.Id == ur.RoleId))
                .ToListAsync();
        }

        public override async Task UpdateAsync(int id, User user)
        {
            var userFromDb = await _context.Users.FirstOrDefaultAsync(p => p.Id == id);

            userFromDb.FirstName = user.FirstName;
            userFromDb.LastName = user.LastName;
            userFromDb.Email = user.Email;

            _context.Users.Update(userFromDb);
        }
    }
}
