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

            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var blogPosts = await _blogPostInterface.GetAllAsync();

            return View(blogPosts);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid Id)
        {
            //Retrieve blog post data thru its Id
            var blogPost = await _blogPostInterface.GetAsync(Id);
            var tagsDomainModel = await _tagInterface.GetAllAsync();

            //Map domain model to the view model
            if(blogPost != null)
            {
                var editBlogRequest = new EditBlogPostRequest()
                {
                    Id = blogPost.Id,
                    Heading = blogPost.Heading,
                    PageTitle = blogPost.PageTitle,
                    Content = blogPost.Content,
                    Author = blogPost.Author,
                    FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    UrlHandle = blogPost.UrlHandle,
                    ShortDescription = blogPost.ShortDescription,
                    PublishedDate = blogPost.PublishDate,
                    Visible = blogPost.Visible,
                    Tags = tagsDomainModel.Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }),
                    SelectedTags = blogPost.Tags.Select(x => x.Id.ToString()).ToArray()
                };

                return View(editBlogRequest);
            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditBlogPostRequest editBlogPostRequest)
        {
            var blogPost = new BlogPost()
            {
                Id = editBlogPostRequest.Id,
                Heading = editBlogPostRequest.Heading,
                PageTitle = editBlogPostRequest.PageTitle,
                Content = editBlogPostRequest.Content,
                Author = editBlogPostRequest.Author,
                ShortDescription = editBlogPostRequest.ShortDescription,
                FeaturedImageUrl = editBlogPostRequest.FeaturedImageUrl,
                PublishDate = editBlogPostRequest.PublishedDate,
                UrlHandle = editBlogPostRequest.UrlHandle,
                Visible = editBlogPostRequest.Visible,
            };

            var selectedTags = new List<Tag>();
            foreach (var selectedTag in editBlogPostRequest.SelectedTags)
            {
                if (Guid.TryParse(selectedTag, out var tag))
                {
                    var foundTag = await _tagInterface.GetAsync(tag);

                    if (foundTag != null)
                    {
                        selectedTags.Add(foundTag);
                    }
                }
            }

            blogPost.Tags = selectedTags;

            var updatedBlog = await _blogPostInterface.UpdateAsync(blogPost);

            if(updatedBlog != null)
            {
                return RedirectToAction("Edit");
            }

            return RedirectToAction("Edit");
        }
    }
}
