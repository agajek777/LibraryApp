using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibApp.Validation
{
    public class TargetDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if ((DateTime)value > DateTime.Now)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Invalid date, please retype Target Date");
            }
        }

        public override string FormatErrorMessage(string name)
        {
            return base.FormatErrorMessage("Invalid date, please retype Target Date");
        }
    }
}
