using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace AspBlog.Models
{
    public class LoginViewModel
    {
    

        [Required]
        [EmailAddress(ErrorMessage = "Neplatna emailova adresa")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="Heslo")]
        public string Password { get; set; }
        
        [Display(Name = "Pamätaj si ma")]
        public bool RememberMe { get; set; }
        

  

    }
}
