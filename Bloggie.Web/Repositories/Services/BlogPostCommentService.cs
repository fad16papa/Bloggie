using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Bloggie.Web.Repositories.Interfaces;

namespace Bloggie.Web.Repositories.Services
{
    public class BlogPostCommentService : IBlogPostCommentInterface
    {
        private readonly BloggieDbContext _bloggieDbContext;

        public BlogPostCommentService(BloggieDbContext bloggieDbContext)
        {
            _bloggieDbContext = bloggieDbContext;
        }

        public async Task<BlogPostComment> AddAsync(BlogPostComment blogPostComment)
        {
            await _bloggieDbContext.BlogPostComment.AddAsync(blogPostComment);
            
            await _bloggieDbContext.SaveChangesAsync();

            return blogPostComment;
        }
    }
}
