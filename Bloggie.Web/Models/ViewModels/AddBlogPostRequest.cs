using System.ComponentModel.DataAnnotations;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bloggie.Web.Models.ViewModels
{
    public class AddBlogPostRequest
    {
        [Required]
        public string Heading { get; set; }
        [Required]
        public string PageTitle { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string ShortDescription { get; set; }
        [Required]
        public string FeaturedImageUrl { get; set; }
        [Required]
        public string UrlHandle { get; set; }
        [Required]
        [DataType(DataType.DateTime, ErrorMessage = "Invalid DateTime format.")]
        public DateTime PublishedDate { get; set; }
        [Required]
        public string Author { get; set; }
        public bool Visible { get; set; }

        //Display Tags
        public IEnumerable<SelectListItem> Tags { get; set; }

        [Required(ErrorMessage = "Please select a tag")]
        [MinLength(1, ErrorMessage = "Please select a tag")]
        //Collect Tag
        public string[] SelectedTags { get; set; } = Array.Empty<string>();
    }
}
