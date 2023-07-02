using Bloggie.Web.Models;
using Bloggie.Web.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Bloggie.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogPostInterface _blogPostInterface;

        public HomeController(ILogger<HomeController> logger, IBlogPostInterface blogPostInterface)
        {
            _logger = logger;
            _blogPostInterface = blogPostInterface;
        }

        public async Task<IActionResult> Index()
        {
            var blogPosts = await _blogPostInterface.GetAllAsync();

            return View(blogPosts);
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