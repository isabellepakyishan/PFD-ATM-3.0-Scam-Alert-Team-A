using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PFD_ATM_3._0_Team_A.DAL;
using PFD_ATM_3._0_Team_A.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PFD_ATM_3._0_Team_A.Controllers
{
    public class ExceedAvgWithdrawalController : Controller
    {
        private AccountsDAL accountContext = new AccountsDAL();
        private WithdrawalRecordsDAL withdrawalContext = new WithdrawalRecordsDAL();
        
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult ReturnToEnterWithdrawalAmount()
        {
            return RedirectToAction("Index", "EnterWithdrawalAmount");
        }

        public ActionResult ProceedWithWithdrawal()
        {
            string accountNo = HttpContext.Session.GetString("AccountNo");
            Accounts retrievedAccount = accountContext.GetAccount(accountNo);
            
            int intendedWithdrawalAmount = (int)HttpContext.Session.GetInt32("WithdrawalAmount"); ;
            decimal accountBalance = retrievedAccount.Balance;
            decimal avgWithdrawal = retrievedAccount.AvgWithdrawal;

            var enteredPin = HttpContext.Session.GetString("EnteredPinNo");
            var storedPin = HttpContext.Session.GetString("PinNo");

            if (enteredPin.Length == 6)
            {
                if (enteredPin == storedPin)
                {
                    int timesWithdrawn = retrievedAccount.TimesWithdrawn;
                    int newTimesWithdrawn = retrievedAccount.TimesWithdrawn += 1;
                    decimal newAvgWithdrawal = (avgWithdrawal * timesWithdrawn + intendedWithdrawalAmount) / newTimesWithdrawn;
                    bool avgExceeded = intendedWithdrawalAmount > avgWithdrawal;
                    decimal finalBalance = retrievedAccount.Balance - intendedWithdrawalAmount;
                    DateTime withdrawalDate = DateTime.Now;

                    if (ModelState.IsValid)
                    {
                        accountContext.WithdrawalUpdateAccountDetails(accountNo, finalBalance, newAvgWithdrawal, newTimesWithdrawn);
                        withdrawalContext.InsertWithdrawalRecord(withdrawalDate, accountNo, intendedWithdrawalAmount, avgExceeded, false);
                    }
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
        }
    }
}
