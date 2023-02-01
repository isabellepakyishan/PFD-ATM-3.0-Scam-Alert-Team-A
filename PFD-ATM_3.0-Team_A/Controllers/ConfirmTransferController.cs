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
        public IActionResult Index()
        {
            //Session["AccountNo"]
            return View();
        }

        public ActionResult CheckPIN()
        {
            var enteredPin = HttpContext.Session.GetString("EnteredPinNo");
            var storedPin = HttpContext.Session.GetString("PinNo");

            if (enteredPin.Length == 6)
            {
                if (enteredPin == storedPin)
                {
                    return RedirectToAction("Index", "DispenseCash");
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
            return null;
        }

        public ActionResult ProceedWithTransfer()
        {
            string accountNo = HttpContext.Session.GetString("AccountNo");
            string enteredPinNo = HttpContext.Session.GetString("EnteredPinNo");
            Accounts retrievedAccount = accountContext.GetAccount(accountNo);

            decimal intendedTransferAmount = Convert.ToDecimal(HttpContext.Session.GetInt32("TransferAmount"));
            decimal accountTransferLimit = retrievedAccount.TransferLimit;

            if (intendedTransferAmount > accountTransferLimit)
            {
                return RedirectToAction("Index", "EnterTransferAmount");
            }
            else
            {
                CheckPIN();
            }
            return RedirectToAction();
        }
    }
}
