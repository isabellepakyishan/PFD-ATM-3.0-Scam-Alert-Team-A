using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PFD_ATM_3._0_Team_A.DAL;
using PFD_ATM_3._0_Team_A.Models;

namespace PFD_ATM_3._0_Team_A.Controllers
{
    public class IncorrectPinNoController : Controller
    {
        private AccountsDAL accountContext = new AccountsDAL();
        public IActionResult Index()
        {
            //int? counter = HttpContext.Session.GetInt32("PinEntryCount");

            //if (counter == null)
            //    HttpContext.Session.SetInt32("PinEntryCount", 1);
            //else HttpContext.Session.SetInt32("PinEntryCount", (int)(counter + 1));

            return View();
        }
        public ActionResult CheckPINAgain(IFormCollection form)
        {
            string pin1 = form["pin1"];
            string pin2 = form["pin2"];
            string pin3 = form["pin3"];
            string pin4 = form["pin4"];
            string pin5 = form["pin5"];
            string pin6 = form["pin6"];
            string pin7 = form["pin7"];

            string reenteredPin = pin1 + pin2 + pin3 + pin4 + pin5 + pin6 + pin7; 
            var storedPin = HttpContext.Session.GetString("PinNo");

            if (reenteredPin == storedPin)
            {
                string accountNo = HttpContext.Session.GetString("AccountNo");
                Accounts retrievedAccount = accountContext.GetAccount(accountNo);

                int intendedWithdrawalAmount = (int)HttpContext.Session.GetInt32("WithdrawalAmount");
                decimal finalBalance = retrievedAccount.Balance - intendedWithdrawalAmount;

                if (ModelState.IsValid)
                {
                    accountContext.UpdateAccountBalance(accountNo, finalBalance);
                }
                return RedirectToAction("Index", "DispenseCash");
            }
            else
            {
                return RedirectToAction("Index", "IncorrectPinNo");
            }
        }
    }
}
