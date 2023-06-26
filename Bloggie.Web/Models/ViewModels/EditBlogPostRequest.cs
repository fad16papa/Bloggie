using Bloggie.Web.Controllers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bloggie.Web.Models.ViewModels
{
    public class EditBlogPostRequest : AddBlogPostRequest
    {
        public Guid Id { get; set; }
    }
}
