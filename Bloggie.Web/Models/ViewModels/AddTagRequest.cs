using System.ComponentModel.DataAnnotations;
using Bloggie.Web.Utils;

namespace Bloggie.Web.Models.ViewModels
{
    public class AddTagRequest
    {
        [Required]
        [StringMatchesPropertyAttribute("DisplayName")]
        [MaxLength(20, ErrorMessage = "The maximum allowed character is 20")]
        public string Name { get; set; }

        [Required]
        [StringMatchesPropertyAttribute("Name")]
        [MaxLength(20, ErrorMessage = "The maximum allowed character is 20")]
        public string DisplayName { get; set; }
    }
}
