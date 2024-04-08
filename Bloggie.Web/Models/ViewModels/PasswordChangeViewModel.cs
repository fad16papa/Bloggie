using System.ComponentModel.DataAnnotations;
using Bloggie.Web.Utils;

namespace Bloggie.Web.Models.ViewModels
{
    public class PasswordChangeViewModel
    {
        [Required]
        [EmailAddress]
        [CheckUserEmailExist(ErrorMessage = "This is email was not yet registered.")]
        public string Email { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "Password has a minumum of 8 characters")]
        [MaxLength(30, ErrorMessage = "Password has a maximum of 30 characters")]
        public string Password { get; set; }

        [Required]
        [ConfirmPasswordAttribute("Password")]
        [MinLength(8, ErrorMessage = "ConfirmPassword has a minumum of 8 characters")]
        [MaxLength(30, ErrorMessage = "ConfirmPassword has a maximum of 30 characters")]
        public string ConfirmPassword { get; set; }
    }
}