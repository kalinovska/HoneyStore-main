using HoneyStore.DataAccess.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace HoneyStore.DataAccess.Identity
{
    public class User : IdentityUser<int>, IIdentifier
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
