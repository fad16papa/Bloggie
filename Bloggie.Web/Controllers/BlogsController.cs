using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostInterface _blogPostInterface;
        private readonly IBlogPostLikeInterface _blogPostLikeInterface;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public BlogsController(IBlogPostInterface blogPostInterface, IBlogPostLikeInterface blogPostLikeInterface, 
            SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _blogPostInterface = blogPostInterface;
            _blogPostLikeInterface = blogPostLikeInterface;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {
            var liked = false;
            var blogPost = await _blogPostInterface.GetByUrlHandeAsync(urlHandle);
            var blogDetailsViewModel = new BlogDetailsViewModel();

            if (blogPost != null)
            {
                var totalLikes = await _blogPostLikeInterface.GetTotalLikes(blogPost.Id);

                if(_signInManager.IsSignedIn(User))
                {
                    //Get like for this blog for this user
                    var likesForBlog = await _blogPostLikeInterface.GetLikesForBlog(blogPost.Id);

                    var userId = _userManager.GetUserId(User);

                    if(userId != null)
                    {
                        var likesFromUser = likesForBlog.FirstOrDefault(x => x.UserId == Guid.Parse(userId));
                        liked = likesFromUser != null;
                    }
                }

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
                    Liked = liked,
                };

                blogDetailsViewModel.TotalLikes = totalLikes;
            }

            return View(blogDetailsViewModel);
        }
    }
}
