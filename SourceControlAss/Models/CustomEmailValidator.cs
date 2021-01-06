using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace SourceControlAss.Models
{
    public class CustomEmailValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var email = value.ToString();
                if (Regex.IsMatch(email, @"^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$", RegexOptions.IgnoreCase))
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("Enter a Valid Email...");
                }
            }
            else
            {

                return new ValidationResult("required");

            }
        }
    }
}