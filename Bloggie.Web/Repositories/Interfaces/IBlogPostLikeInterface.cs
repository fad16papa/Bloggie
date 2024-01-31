using Bloggie.Web.Models.Domain;

namespace Bloggie.Web.Repositories.Interfaces
{
    public interface IBlogPostLikeInterface
    {
        Task<int> GetTotalLikes(Guid blogPostId);

        Task<IEnumerable<BlogPostLike>> GetLikesForBlog(Guid blogPostId);

        Task<BlogPostLike> AddLikeForBlog(BlogPostLike blogPostLike);
    }
}
