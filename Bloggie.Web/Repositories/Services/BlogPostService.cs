using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Bloggie.Web.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Repositories.Services
{
    public class BlogPostService : IBlogPostInterface
    {
        private readonly BloggieDbContext _bloggieDbContext;

        public BlogPostService(BloggieDbContext bloggieDbContext)
        {
            _bloggieDbContext = bloggieDbContext;
        }

        public async Task<BlogPost> AddAsync(BlogPost entiy)
        {
            await _bloggieDbContext.BlogPosts.AddAsync(entiy);
            await _bloggieDbContext.SaveChangesAsync();
            return entiy;   
        }

        public async Task<BlogPost> DeleteAsync(Guid id)
        {
            var blogPost = await _bloggieDbContext.BlogPosts.FindAsync(id);

            if (blogPost != null)
            {
                _bloggieDbContext.Remove(blogPost);
                await _bloggieDbContext.SaveChangesAsync();
                return blogPost;
            }

            return null;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await _bloggieDbContext.BlogPosts.Include(x => x.Tags).ToListAsync();
        }

        public async Task<BlogPost> GetAsync(Guid id)
        {
            return await _bloggieDbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<BlogPost> GetByUrlHandeAsync(string urlHandle)
        {
            return await _bloggieDbContext.BlogPosts.Include(x => x.Tags)
                .FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);
        }

        public async Task<BlogPost> UpdateAsync(BlogPost entity)
        {
            var blogPost = await _bloggieDbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == entity.Id);

            if(blogPost != null)
            {
                blogPost.Id = entity.Id;
                blogPost.Heading = entity.Heading ?? blogPost.Heading;
                blogPost.PageTitle = entity.PageTitle ?? blogPost.PageTitle;
                blogPost.Content = entity.Content ?? blogPost.Content;
                blogPost.ShortDescription = entity.ShortDescription ?? blogPost.ShortDescription;
                blogPost.FeaturedImageUrl = entity.FeaturedImageUrl ?? blogPost.FeaturedImageUrl;
                blogPost.UrlHandle = entity.UrlHandle ?? blogPost.UrlHandle;
                blogPost.Author = entity.Author ?? blogPost.Author;
                blogPost.PublishedDate = entity.PublishedDate;
                blogPost.Visible = entity.Visible;
                blogPost.Tags = entity.Tags;

                await _bloggieDbContext.SaveChangesAsync();

                return blogPost;
            }

            return null;
        }
    }
}
