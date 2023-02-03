using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PFD_ATM_3._0_Team_A.DAL;
using PFD_ATM_3._0_Team_A.Models;
using System;

namespace PFD_ATM_3._0_Team_A.Controllers
{
    public class MainMenuController : Controller
    {
        private AccountsDAL accountContext = new AccountsDAL();
        private WithdrawalRecordsDAL withdrawalContext = new WithdrawalRecordsDAL(); 
        
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult OtherServices()
        {
            return View();
        }

        public ActionResult BalanceEnquiry()
        {
            HttpContext.Session.SetString("TransactionType", "BalanceEnquiry");

            var enteredPin = HttpContext.Session.GetString("EnteredPinNo");
            var storedPin = HttpContext.Session.GetString("PinNo");

            string accountNo = HttpContext.Session.GetString("AccountNo");
            Accounts retrievedAccount = accountContext.GetAccount(accountNo);
            decimal accountBalance = Math.Round(retrievedAccount.Balance, 2);

            HttpContext.Session.SetString("AccountBalance", Convert.ToString(accountBalance));

            if (enteredPin.Length == 6)
            {
                if (enteredPin == storedPin)
                {
                    return RedirectToAction("Index", "BalanceEnquiry");
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

        //public ActionResult CheckPIN(string pin)
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
        //}
    }
}