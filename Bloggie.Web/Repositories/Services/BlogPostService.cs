using Bloggie.Web.Models.Domain;
using Bloggie.Web.Repositories.Interfaces;

namespace Bloggie.Web.Repositories.Services
{
    public class BlogPostService : IBlogPostInterface
    {
        public Task<Tag> AddAsync(Tag tag)
        {
            throw new NotImplementedException();
        }

        public Task<Tag> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Tag>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Tag> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Tag> UpdateAsync(Tag tag)
        {
            throw new NotImplementedException();
        }
    }
}
