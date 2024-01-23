namespace Bloggie.Web.Repositories.Interfaces
{
    public interface IBlogPostLikeRepository
    {
        Task<int> GetTotalLikes(Guid blogPostId);
    }
}
