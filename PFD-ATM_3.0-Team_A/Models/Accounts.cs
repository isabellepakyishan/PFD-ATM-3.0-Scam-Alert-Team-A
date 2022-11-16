using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PFD_ATM_3._0_Team_A.Models
{
    public class Accounts
    {
        [Display(Name = "Account Number")]
        [Required(ErrorMessage = "Account Number must be 12 digits")]
        [StringLength(12, MinimumLength = 12)]
        public string AccountNo { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [StringLength(9, MinimumLength = 9)]
        public string NRIC { get; set; }

        public string Contact { get; set; }

        [StringLength(6, MinimumLength = 6)]
        public string Pin { get; set; }

        [DataType(DataType.Currency)]
        public decimal Balance { get; set; }

        [DataType(DataType.Currency)]
        public decimal WithdrawalLimit { get; set; }

        [DataType(DataType.Currency)]
        public decimal TransferLimit { get; set; }
    }
}
