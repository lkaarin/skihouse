﻿using System.ComponentModel.DataAnnotations;

namespace SkiHouse.Web.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [UIHint("password")]
        public string Password { get; set; }
        public string ReturnUrl { get; set; } = "/";
    }
}
