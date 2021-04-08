using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace LaTuaPizza
{
    class PhoneValidate : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value,
                            ValidationContext validationContext)
        {
            string str = value.ToString();

            if (!IsUSorCanadianPhone(str))
            {
                return new ValidationResult("Invalid Phone number");
            }

            return ValidationResult.Success;
        }

        private static bool IsUSorCanadianPhone(string phone)
        {
            //string pattern = @"^(902|204|306|403|416|418|514|604|613)[0-9]{7}$";
            bool isValidUsOrCanadianPhone = new Regex(@"^(902|204|306|403|416|418|514|604|613)[0-9]{7}$").IsMatch(phone);
            return isValidUsOrCanadianPhone;
        }

    }


}

