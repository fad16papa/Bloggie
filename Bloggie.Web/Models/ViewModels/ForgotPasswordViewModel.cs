using System.ComponentModel.DataAnnotations;
using Bloggie.Web.Utils;

namespace Bloggie.Web.Models.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [CheckUserEmailExist(ErrorMessage = "This is email was not yet registered.")]
        public string Email { get; set; }
    }
}