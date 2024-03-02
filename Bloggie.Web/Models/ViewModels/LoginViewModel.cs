using System.ComponentModel.DataAnnotations;

namespace Bloggie.Web.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [MinLength(8, ErrorMessage = "Username 8 minimum characters allowed")]
        [MaxLength(20, ErrorMessage = "Username 20 maximum characters allowed")]
        public string UserName { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Password has a minumum of 6 characters")]
        [MaxLength(30, ErrorMessage = "Password has a maximum of 30 characters")]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }
}
