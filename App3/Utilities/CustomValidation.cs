using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App3.Utilities
{
    public class CustomValidationCheck : ValidationAttribute
    {
        private readonly string allowedDomain;

        public CustomValidationCheck(string allowedDomain)
        {
            this.allowedDomain = allowedDomain;
        }
        public override bool IsValid(object value)
        {
          string[] strArray = value.ToString().Split('@');
         return strArray[1].ToUpper() == allowedDomain.ToUpper();
        }
    }
}
