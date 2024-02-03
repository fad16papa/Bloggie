using Microsoft.AspNetCore.Identity;

namespace Bloggie.Web.Repositories.Interfaces
{
    public interface IUserInterface
    {
        Task<IEnumerable<IdentityUser>> GetAll();
    }
}
