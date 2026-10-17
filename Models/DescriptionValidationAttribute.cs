using System;
using System.ComponentModel.DataAnnotations;

namespace ProductsValidation.Models
{
    public class DescriptionValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            var description = value as string;

            if (string.IsNullOrEmpty(description))
                return ValidationResult.Success;

            var product = (Product)context.ObjectInstance;

            if (string.IsNullOrEmpty(product.Name))
                return ValidationResult.Success;

            if (description == product.Name)
                return new ValidationResult("Description must not be equal to Name");

            if (!description.StartsWith(product.Name, StringComparison.Ordinal))
                return new ValidationResult("Description must start with the Name of the product");

            return ValidationResult.Success;
        }
    }
}