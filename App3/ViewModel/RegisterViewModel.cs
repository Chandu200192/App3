using App3.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App3.ViewModel
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Remote(action: "IsEmailInUse", controller: "Account", ErrorMessage ="Alreay Exists")]
        [CustomValidationCheck(allowedDomain:"test.com", ErrorMessage ="Invalid Domain")]        
        public string Email { get; set; }
         [Required]
         [DataType(DataType.Password)]
        public string Password { get; set; }
       [Required]
       [DataType(DataType.Password)]
       [Display(Name ="Confirm Password")]
       [Compare("Password", ErrorMessage ="Password and Confirm Password do not match!")]
        public string ConfirmPassword { get; set; }
    }
}
