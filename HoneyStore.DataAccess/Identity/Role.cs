using HoneyStore.DataAccess.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace HoneyStore.DataAccess.Identity
{
    public class Role : IdentityRole<int>, IIdentifier
    {
        //public virtual ICollection<User> Users { get; set; }
    }
}
