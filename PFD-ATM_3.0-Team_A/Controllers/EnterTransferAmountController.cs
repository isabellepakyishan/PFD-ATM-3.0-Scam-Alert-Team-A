using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PFD_ATM_3._0_Team_A.DAL;
using PFD_ATM_3._0_Team_A.Models;
using System;

namespace PFD_ATM_3._0_Team_A.Controllers
{
    public class EnterTransferAmountController : Controller
    {
        private AccountsDAL accountContext = new AccountsDAL();
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult CheckTransferAmount(IFormCollection form)
        {
            string accountNo = HttpContext.Session.GetString("AccountNo");
            Accounts retrievedAccount = accountContext.GetAccount(accountNo);

            decimal intendedTransferAmount = Convert.ToDecimal(form["transferAmount"]);
            decimal accountTransferLimit = retrievedAccount.TransferLimit;
            decimal accountBalance = retrievedAccount.Balance;

            if (intendedTransferAmount > accountTransferLimit)
            {
                TempData["Message"] = "Transfer amount entered exceeds daily transfer limit. Please enter a valid funds transfer amount.";
                return RedirectToAction("Index", "EnterTransferAmount");
            }
            else if (intendedTransferAmount > accountBalance)
            {
                TempData["Message"] = "Transfer amount entered exceeds your account balance. Please enter a valid withdrawal amount.";
                return RedirectToAction("Index", "EnterWithdrawalAmount");
            }
            else
            {
                string transferAmountString = Convert.ToString(intendedTransferAmount);
                HttpContext.Session.SetString("TransferAmount", transferAmountString);
                return RedirectToAction("Index", "ConfirmTransfer");
            }
        }

        //public ActionResult StoreTransferAmount(IFormCollection form)
        //{
        //    int transferAmount = Convert.ToInt32(form["transferAmount"]);
        //    HttpContext.Session.SetInt32("TransferAmount", transferAmount);
        //    return RedirectToAction("Index", "ConfirmTransfer");
        //}
    }
}