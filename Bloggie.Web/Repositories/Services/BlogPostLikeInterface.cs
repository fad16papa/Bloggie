using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Bloggie.Web.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Repositories.Services
{
    public class BlogPostLikeInterface : IBlogPostLikeInterface
    {
        private readonly BloggieDbContext _bloggieDbContext;

        public BlogPostLikeInterface(BloggieDbContext bloggieDbContext)
        {
            _bloggieDbContext = bloggieDbContext;
        }

        public async Task<BlogPostLike> AddLikeForBlog(BlogPostLike blogPostLike)
        {
            await _bloggieDbContext.BlogPostLike.AddAsync(blogPostLike);
            await _bloggieDbContext.SaveChangesAsync();

            return blogPostLike;
        }

        public async Task<IEnumerable<BlogPostLike>> GetLikesForBlog(Guid blogPostId)
        {
            return await _bloggieDbContext.BlogPostLike.Where(x => x.BlogPostId == blogPostId).ToListAsync();
        }

        public async Task<int> GetTotalLikes(Guid blogPostId)
        {
            return await _bloggieDbContext.BlogPostLike.CountAsync(x => x.BlogPostId == blogPostId);
        }
    }
}
