using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using PFD_ATM_3._0_Team_A.DAL;

namespace PFD_ATM_3._0_Team_A.Models
{
    public class ValidateAccountExists : ValidationAttribute
    {
        private AccountsDAL accountContext = new AccountsDAL();
        protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
        {
            // Get the email value to validate
            string accountNo = Convert.ToString(value);
            // Casting the validation context to the "Account" model class
            Accounts account = (Accounts)validationContext.ObjectInstance;
            if (accountContext.IsAccountExist(accountNo))
                // validation passed 
                return ValidationResult.Success;
            else
                // validation failed
                return new ValidationResult
                    ("Account does not exist");
        }
    }
}