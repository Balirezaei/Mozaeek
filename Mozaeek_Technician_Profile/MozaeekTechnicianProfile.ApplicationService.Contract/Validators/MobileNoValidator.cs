using MozaeekTechnicianProfile.Common.ExtensionMethod;
using System;
using System.ComponentModel.DataAnnotations;

namespace MozaeekTechnicianProfile.ApplicationService.Contract.Validators
{
    public class ValidateMobileNoAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            var mobileNo = value.ToString();
            try
            {
                mobileNo.CheckIsMobileNo();
            }
            catch (System.Exception ex)
            {
                return new ValidationResult(ex.Message);
            }

            return ValidationResult.Success;
        }
    }
}
