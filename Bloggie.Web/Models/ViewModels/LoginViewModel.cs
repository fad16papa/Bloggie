﻿using System.ComponentModel.DataAnnotations;

namespace Bloggie.Web.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Password has a minumum of 6 characters")]
        [MaxLength(20, ErrorMessage = "Password has a maximum of 20 characters")]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }
}
