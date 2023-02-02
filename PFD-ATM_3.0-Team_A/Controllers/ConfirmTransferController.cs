using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PFD_ATM_3._0_Team_A.DAL;
using PFD_ATM_3._0_Team_A.Models;
using System;

namespace PFD_ATM_3._0_Team_A.Controllers
{
    public class ConfirmTransferController : Controller
    {
        private AccountsDAL accountContext = new AccountsDAL();
        private TransferRecordsDAL transferContext = new TransferRecordsDAL();
        
        public IActionResult Index()
        {
            return View();
        }


        public ActionResult ProceedWithTransfer()
        {
            string accountNo = HttpContext.Session.GetString("AccountNo");
            string transferAccountNo = HttpContext.Session.GetString("TransferAccountNo");
            Accounts retrievedAccount = accountContext.GetAccount(accountNo);
            Accounts retrievedTransferAccount = accountContext.GetAccount(transferAccountNo);
            decimal intendedTransferAmount = Convert.ToDecimal(HttpContext.Session.GetString("TransferAmount"));

            string enteredPin = HttpContext.Session.GetString("EnteredPinNo");
            string storedPin = HttpContext.Session.GetString("PinNo");

            if (enteredPin.Length == 6)
            {
                if (enteredPin == storedPin)
                {
                    decimal finalBalance = retrievedAccount.Balance - intendedTransferAmount;
                    decimal newBalance = retrievedTransferAccount.Balance + intendedTransferAmount;

                    if (ModelState.IsValid)
                    {
                        accountContext.TransferUpdateAccountBalance(accountNo, finalBalance, transferAccountNo, newBalance);
                        transferContext.InsertTransferRecord(accountNo, transferAccountNo, intendedTransferAmount, false);
                    }
                    return RedirectToAction("Index", "SuccessfulTransfer");
                }
                else
                {
                    return RedirectToAction("Index", "IncorrectPinNo");
                }
            }
            else
            {
                char lastPin = enteredPin[enteredPin.Length - 1];
                string first6Pin = enteredPin.Remove(6);
                if (lastPin == '*' && first6Pin == storedPin)
                {
                    return RedirectToAction("Index", "AuthenticationError");
                }
                else
                {
                    return RedirectToAction("Index", "IncorrectPinNo");
                }
            }
        }

        //public ActionResult CheckPIN()
        //{
        //    var enteredPin = HttpContext.Session.GetString("EnteredPinNo");
        //    var storedPin = HttpContext.Session.GetString("PinNo");

        //    if (enteredPin.Length == 6)
        //    {
        //        if (enteredPin == storedPin)
        //        {
        //            return RedirectToAction("Index", "DispenseCash");
        //        }
        //        else
        //        {
        //            return RedirectToAction("Index", "IncorrectPinNo");
        //        }
        //    }
        //    else
        //    {
        //        char lastPin = enteredPin[enteredPin.Length - 1];
        //        string first6Pin = enteredPin.Remove(6);
        //        if (lastPin == '*' && first6Pin == storedPin)
        //        {
        //            return RedirectToAction("Index", "AuthenticationError");
        //        }
        //        else
        //        {
        //            return RedirectToAction("Index", "IncorrectPinNo");
        //        }
        //    }
        //    return null;
        //}    '
        }
}
