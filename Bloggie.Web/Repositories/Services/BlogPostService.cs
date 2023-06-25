using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Bloggie.Web.Repositories.Interfaces;

namespace Bloggie.Web.Repositories.Services
{
    public class BlogPostService : IBlogPostInterface
    {
        private readonly BloggieDbContext _bloggieDbContext;

        public BlogPostService(BloggieDbContext bloggieDbContext)
        {
            _bloggieDbContext = bloggieDbContext;
        }

        public Task<BlogPost> AddAsync(BlogPost entiy)
        {
            throw new NotImplementedException();
        }

        public Task<BlogPost> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<BlogPost> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<BlogPost> UpdateAsync(BlogPost entity)
        {
            throw new NotImplementedException();
        }
    }
}
