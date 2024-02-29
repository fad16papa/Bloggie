using System.ComponentModel.DataAnnotations;
using Bloggie.Web.Models.Domain;
using Bloggie.Web.Utils;

namespace Bloggie.Web.Models.ViewModels
{
    public class EditTagRequest
    {
        public Guid Id { get; set; }

        [Required]
        [StringMatchesProperty("DisplayName")]
        public string Name { get; set; }

        [Required]
        [StringMatchesProperty("Name")]
        public string DisplayName { get; set; }

        public ICollection<BlogPost> BlogPosts { get; set; }
    }
}
