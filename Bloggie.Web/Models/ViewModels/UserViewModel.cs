using System.ComponentModel.DataAnnotations;
using Bloggie.Web.Utils;

namespace Bloggie.Web.Models.ViewModels
{
    public class UserViewModel
    {
        public List<User> Users { get; set; }

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
        [MinLength(8, ErrorMessage = "Password 8 minimum characters allowed")]
        [MaxLength(30, ErrorMessage = "Password 30 maximum characters allowed")]
        public string Password { get; set; }

        public bool AdminRole { get; set; }
    }
}