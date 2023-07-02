using Bloggie.Web.Models;
using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Bloggie.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogPostInterface _blogPostInterface;
        private readonly ITagInterface _tagInterface;

        public HomeController(ILogger<HomeController> logger, IBlogPostInterface blogPostInterface, ITagInterface tagInterface)
        {
            _logger = logger;
            _blogPostInterface = blogPostInterface;
            _tagInterface = tagInterface;
        }

        public async Task<IActionResult> Index()
        {
            //getting all blogs
            var blogPosts = await _blogPostInterface.GetAllAsync();

            //get all tags
            var tags = await _tagInterface.GetAllAsync();

            var homeViewModel = new HomeViewModel()
            {
                BlogPosts = blogPosts,
                Tags = tags
            };

            return View(homeViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}