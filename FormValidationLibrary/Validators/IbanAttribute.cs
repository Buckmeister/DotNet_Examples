using System;
using System.ComponentModel.DataAnnotations;

namespace FormValidationLibrary.Validators
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class IbanAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var inputValue = value as string;
            
            var ibanValidator = new IbanValidator();

            if (ibanValidator.Validate(inputValue) == IbanValidator.ValidationResult.IsValid)
            {
                return true;
            }
            else
            {
                Console.WriteLine("IBAN ValidationResult: {0}", ibanValidator.Validate(inputValue).ToString());
                return false;
            }
        }
    }
}
