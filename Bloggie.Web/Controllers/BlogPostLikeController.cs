using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostLikeController : ControllerBase
    {
        private readonly IBlogPostLikeInterface _blogPostLikeInterface;

        public BlogPostLikeController(IBlogPostLikeInterface blogPostLikeInterface)
        {
            _blogPostLikeInterface = blogPostLikeInterface;
        }

        [HttpPost]
        [Route("Add")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddLike([FromBody] AddLikeRequest addLikeRequest)
        {
            var blogPostLike = new BlogPostLike()
            {
                BlogPostId = addLikeRequest.BlogPostId,
                UserId = addLikeRequest.UserId
            };

            await _blogPostLikeInterface.AddLikeForBlog(blogPostLike);

            return Ok();
        }

        [HttpGet]
        [Route("{blogPostId:Guid}/totalLikes")]
        public async Task<IActionResult> GetTotalLikesForBLog([FromRoute] Guid blogPostId)
        {
            var totalLikes = await _blogPostLikeInterface.GetTotalLikes(blogPostId);

            return Ok(totalLikes);
        }
    }
}
