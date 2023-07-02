using Bloggie.Web.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostInterface _blogPostInterface;

        public BlogsController(IBlogPostInterface blogPostInterface)
        {
            _blogPostInterface = blogPostInterface;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {
            var blogPost = await _blogPostInterface.GetByUrlHandeAsync(urlHandle);
            return View(blogPost);
        }
    }
}
