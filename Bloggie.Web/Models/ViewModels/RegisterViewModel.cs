using System.ComponentModel.DataAnnotations;

namespace Bloggie.Web.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Password has a minumum of 6 characters")]
        [MaxLength(12, ErrorMessage = "Password has a maximum of 20 characters")]
        public string Password { get; set; }
    }
}
