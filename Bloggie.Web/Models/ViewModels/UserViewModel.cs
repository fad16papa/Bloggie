using System.ComponentModel.DataAnnotations;
using System.Reflection.PortableExecutable;

namespace Bloggie.Web.Models.ViewModels
{
    public class UserViewModel
    {
        public List<User> Users { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public bool AdminRole { get; set; }
    }
}
