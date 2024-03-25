using Bloggie.Web.Data;
using Bloggie.Web.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Repositories.Services
{
    public class UserService : IUserInterface
    {
        private readonly AuthDbContext _authDbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly BloggieDbContext _bloggieDbContext;
        private readonly IConfiguration _configuration;

        public UserService(AuthDbContext authDbContext, UserManager<IdentityUser> userManager, BloggieDbContext bloggieDbContext, IConfiguration configuration)
        {
            _configuration = configuration;
            _bloggieDbContext = bloggieDbContext;
            _userManager = userManager;
            _authDbContext = authDbContext;
        }

        public async Task<IdentityResult> DeleteUserAsync(IdentityUser user)
        {
            IdentityResult identityResult = null;

            if (user != null)
            {
                identityResult = await _userManager.DeleteAsync(user);

                using var transaction = _bloggieDbContext.Database.BeginTransaction();

                try
                {
                    //Delete all related user data
                    var userData = _bloggieDbContext.BlogPostLike.Where(x => x.UserId == new Guid(user.Id));
                    _bloggieDbContext.BlogPostLike.RemoveRange(userData);

                    //Delete the user 
                    await _userManager.DeleteAsync(user);

                    //Commit the DB transaction 
                    await _bloggieDbContext.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }

            return identityResult;
        }

        public async Task<IEnumerable<IdentityUser>> GetAll()
        {
            var users = await _authDbContext.Users.ToListAsync();

            var superAdminUser = await _authDbContext.Users.FirstOrDefaultAsync(x => x.Email == _configuration["SuperAdmin"]);

            if (superAdminUser != null)
            {
                users.Remove(superAdminUser);
            }

            return users;
        }
    }
}
