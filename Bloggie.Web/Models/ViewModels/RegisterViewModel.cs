using System.ComponentModel.DataAnnotations;
using Bloggie.Web.Utils;
using Microsoft.AspNetCore.Authentication;

namespace Bloggie.Web.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [UniqueUsername(ErrorMessage = "Username is already in use.")]
        [UsernameValidation(ErrorMessage = "Invalid username format.")]
        [MinLength(8, ErrorMessage = "Username 8 minimum characters allowed")]
        [MaxLength(20, ErrorMessage = "Username 20 maximum characters allowed")]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [UniqueEmail(ErrorMessage = "Email is already in use.")]
        public string Email { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "Password has a minumum of 8 characters")]
        [MaxLength(30, ErrorMessage = "Password has a maximum of 30 characters")]
        public string Password { get; set; }
    }
}
