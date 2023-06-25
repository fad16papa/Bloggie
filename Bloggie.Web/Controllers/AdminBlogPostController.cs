using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;

namespace Bloggie.Web.Controllers
{
    public class AdminBlogPostController : Controller
    {
        private readonly ITagInterface _tagInterface;
        private readonly IBlogPostInterface _blogPostInterface;

        public AdminBlogPostController(ITagInterface tagInterface, IBlogPostInterface blogPostInterface)
        {
            _tagInterface = tagInterface;
            _blogPostInterface = blogPostInterface;
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
            var blogPost = new BlogPost()
            {
                Heading = addBlogPostRequest.Heading,
                PageTitle = addBlogPostRequest.PageTitle,
                Content = addBlogPostRequest.Content,
                ShortDescription = addBlogPostRequest.ShortDescription,
                FeaturedImageUrl = addBlogPostRequest.FeaturedImageUrl,
                UrlHandle = addBlogPostRequest.UrlHandle,
                PublishDate = addBlogPostRequest.PublishedDate,
                Author = addBlogPostRequest.Author,
                Visible = addBlogPostRequest.Visible,
            };

            //Maps Tags form selected tags
            var selectedTags = new List<Tag>();
            foreach (var selectedTag in addBlogPostRequest.SelectedTags)
            {
                var selectedTagIdAsGuid = Guid.Parse(selectedTag);
                var existingTags = await _tagInterface.GetAsync(selectedTagIdAsGuid);

                if(existingTags != null)
                {
                    selectedTags.Add(existingTags);
                }

                blogPost.Tags = selectedTags;
            }

            await _blogPostInterface.AddAsync(blogPost);

            return RedirectToAction("Add");
        }
    }
}
