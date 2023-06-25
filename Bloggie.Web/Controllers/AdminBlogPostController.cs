using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bloggie.Web.Controllers
{
    public class AdminBlogPostController : Controller
    {
        private readonly ITagInterface _tagInterface;

        public AdminBlogPostController(ITagInterface tagInterface)
        {
            _tagInterface = tagInterface;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var tags = await _tagInterface.GetAllAsync();

            var model = new AddBlogPostRequest
            {
                Tags = tags.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBlogPostRequest addBlogPostRequest)
        {


            return RedirectToAction("Add");
        }
    }
}
