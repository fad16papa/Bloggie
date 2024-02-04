using System.ComponentModel.DataAnnotations;

namespace Bloggie.Web.Models.ViewModels
{
    public class AddTagRequest
    {
        [Required]
        [MaxLength(20, ErrorMessage = "The maximum allowed character is 30")]
        public string Name { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "The maximum allowed character is 30")]
        public string DisplayName { get; set; }
    }
}
