using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostInterface _blogPostInterface;
        private readonly IBlogPostLikeRepository _blogPostLikeRepository;

        public BlogsController(IBlogPostInterface blogPostInterface, IBlogPostLikeRepository blogPostLikeRepository)
        {
            _blogPostInterface = blogPostInterface;
            _blogPostLikeRepository = blogPostLikeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {
            var blogPost = await _blogPostInterface.GetByUrlHandeAsync(urlHandle);
            var blogDetailsViewModel = new BlogDetailsViewModel();

            if (blogPost != null)
            {
                var totalLikes = await _blogPostLikeRepository.GetTotalLikes(blogPost.Id);

                blogDetailsViewModel = new BlogDetailsViewModel()
                {
                    Id = blogPost.Id,
                    Author = blogPost.Author,
                    Content = blogPost.Content,
                    FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    Heading = blogPost.Heading,
                    PageTitle = blogPost.PageTitle,
                    PublishedDate = blogPost.PublishedDate,
                    ShortDescription = blogPost.ShortDescription,
                    Tags = blogPost.Tags,
                    UrlHandle = urlHandle,
                    Visible = blogPost.Visible,
                };
                blogDetailsViewModel.TotalLikes = totalLikes;
            }

            return View(blogDetailsViewModel);
        }
    }
}
