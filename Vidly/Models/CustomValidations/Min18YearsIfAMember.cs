using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly.ViewModels;

namespace Vidly.Models.CustomValidations
{
    public class Min18YearsIfAMember: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Customer customer = (Customer) validationContext.ObjectInstance;

            if (customer.MembershipTypeId == Customer.UNKNOWN || customer.MembershipTypeId == Customer.PAY_AS_YOU_GO)
            {
                return ValidationResult.Success;
            }

            if (customer.Birthdate == null)
            {
                return new ValidationResult("La Fecha de Nacimiento es requerida.");
            }

            var age = DateTime.Today.Year - customer.Birthdate.Value.Year;
            return (age >= 18) 
                ? ValidationResult.Success 
                : new ValidationResult("La edad requerida para tener una membresia son 18 años.");
        }
    }
}