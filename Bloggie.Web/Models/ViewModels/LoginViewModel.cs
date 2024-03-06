using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication;

namespace Bloggie.Web.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Password has a minumum of 6 characters")]
        [MaxLength(30, ErrorMessage = "Password has a maximum of 30 characters")]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }
}
