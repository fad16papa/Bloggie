using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;

namespace Bloggie.Web.Controllers
{
    public class AdminTagController : Controller
    {
        private readonly ITagInterface _tagInterface;

        public AdminTagController(ITagInterface tagInterface)
        {
            _tagInterface = tagInterface;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> SubmitTag(AddTagRequest addTagRequest)
        {
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName
            };

            await _tagInterface.AddAsync(tag);

            return RedirectToAction("List");
        }

        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List()
        {
            var tags = await _tagInterface.GetAllAsync();

            return View(tags);
        }

        [HttpGet]
        [ActionName("Edit")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var tag = await _tagInterface.GetAsync(id);

            if (tag != null)
            {
                var editTagRequest = new EditTagRequest()
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName
                };

                return View(editTagRequest);
            }
            return View(null);
        }

        [HttpPost]
        [ActionName("Edit")]
        public async Task<IActionResult> Edit(EditTagRequest editTagRequest)
        {
            var tag = new Tag()
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName,
            };

            var updatedTag = await _tagInterface.UpdateAsync(tag);

            if (updatedTag != null)
            {
                //show success notification
            }
            else
            {
                //show error notification
            }

            //show failure notification
            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> Delete(EditTagRequest editTagRequest)
        {
            var deletedTag = await _tagInterface.DeleteAsync(editTagRequest.Id);

            if (deletedTag != null)
            {
                //show success notification
                return RedirectToAction("List");
            }

            //show error notification
            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }
    }
}
