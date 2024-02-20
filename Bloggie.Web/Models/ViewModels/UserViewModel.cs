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
        [MinLength(8, ErrorMessage = "8 minimum characters allowed")]
        [MaxLength(20, ErrorMessage = "20 maximum characters allowed")]

        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [UniqueEmail(ErrorMessage = "Email is already in use.")]
        public string Email { get; set; }

        [Required]
        [PasswordValidation(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]$", ErrorMessage = "Password must meet the requirements.")]
        [MinLength(8, ErrorMessage = "8 minimum characters allowed")]
        [MaxLength(30, ErrorMessage = "30 maximum characters allowed")]
        public string Password { get; set; }

        [Required]
        public bool AdminRole { get; set; }
    }
}