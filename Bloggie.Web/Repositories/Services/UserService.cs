using Bloggie.Web.Data;
using Bloggie.Web.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Repositories.Services
{
    public class UserService : IUserInterface
    {
        private readonly AuthDbContext _authDbContext;

        public UserService(AuthDbContext authDbContext)
        {
            _authDbContext = authDbContext;
        }

        public async Task<IEnumerable<IdentityUser>> GetAll()
        {
            var users = await _authDbContext.Users.ToListAsync();

            var superAdminUser = await _authDbContext.Users.FirstOrDefaultAsync(x => x.Email == "superAdmin@bloggie.com");

            if(superAdminUser != null) 
            {
                users.Remove(superAdminUser);
            }

            return users;
        }
    }
}
