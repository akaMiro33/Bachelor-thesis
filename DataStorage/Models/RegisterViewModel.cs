﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspBlog.Models
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Neplatná emailová adresa")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} musí mít délku alespoň {2} a nejvíc {1} znaků.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Heslo")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potvrdenie hesla")]
        [Compare("Password", ErrorMessage = "Zadaná hesla se musí shodovat.")]
        public string ConfirmPassword { get; set; }

        // veci okolo nahravania suborov
        public IFormFile AvatarImage { get; set; }
    }
}