using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PFD_ATM_3._0_Team_A.DAL;
using PFD_ATM_3._0_Team_A.Models;
using System;

namespace PFD_ATM_3._0_Team_A.Controllers
{
    public class DispenseCashController : Controller
    {
        private AccountsDAL accountContext = new AccountsDAL();
        private WithdrawalRecordsDAL withdrawalContext = new WithdrawalRecordsDAL();
        
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult CheckPIN50()
        {
            HttpContext.Session.SetString("TransactionType", "Withdrawal");
            
            string accountNo = HttpContext.Session.GetString("AccountNo");
            Accounts retrievedAccount = accountContext.GetAccount(accountNo);
            
            int intendedWithdrawalAmount = 50;
            decimal accountBalance = retrievedAccount.Balance;

            HttpContext.Session.SetInt32("WithdrawalAmount", intendedWithdrawalAmount);

            var enteredPin = HttpContext.Session.GetString("EnteredPinNo");
            var storedPin = HttpContext.Session.GetString("PinNo");

            if (enteredPin.Length == 6)
            {
                if (enteredPin == storedPin)
                {
                    int timesWithdrawn = retrievedAccount.TimesWithdrawn;
                    int newTimesWithdrawn = retrievedAccount.TimesWithdrawn += 1;
                    decimal newAvgWithdrawal = (retrievedAccount.AvgWithdrawal * timesWithdrawn + intendedWithdrawalAmount) / newTimesWithdrawn;
                    decimal finalBalance = retrievedAccount.Balance - intendedWithdrawalAmount;

                    if (ModelState.IsValid)
                    {
                        accountContext.WithdrawalUpdateAccountDetails(accountNo, finalBalance, newAvgWithdrawal, newTimesWithdrawn);
                        withdrawalContext.InsertWithdrawalRecord(accountNo, intendedWithdrawalAmount, false);
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

        public ActionResult CheckPIN80()
        {
            HttpContext.Session.SetString("TransactionType", "Withdrawal");

            string accountNo = HttpContext.Session.GetString("AccountNo");
            Accounts retrievedAccount = accountContext.GetAccount(accountNo);

            int intendedWithdrawalAmount = 80;
            decimal accountBalance = retrievedAccount.Balance;

            HttpContext.Session.SetInt32("WithdrawalAmount", intendedWithdrawalAmount);

            var enteredPin = HttpContext.Session.GetString("EnteredPinNo");
            var storedPin = HttpContext.Session.GetString("PinNo");

            if (enteredPin.Length == 6)
            {
                if (enteredPin == storedPin)
                {
                    int timesWithdrawn = retrievedAccount.TimesWithdrawn;
                    int newTimesWithdrawn = retrievedAccount.TimesWithdrawn += 1;
                    decimal newAvgWithdrawal = (retrievedAccount.AvgWithdrawal * timesWithdrawn + intendedWithdrawalAmount) / newTimesWithdrawn;
                    decimal finalBalance = retrievedAccount.Balance - intendedWithdrawalAmount;

                    if (ModelState.IsValid)
                    {
                        accountContext.WithdrawalUpdateAccountDetails(accountNo, finalBalance, newAvgWithdrawal, newTimesWithdrawn);
                        withdrawalContext.InsertWithdrawalRecord(accountNo, intendedWithdrawalAmount, false);
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

        public ActionResult CheckPIN100()
        {
            HttpContext.Session.SetString("TransactionType", "Withdrawal");

            string accountNo = HttpContext.Session.GetString("AccountNo");
            Accounts retrievedAccount = accountContext.GetAccount(accountNo);

            int intendedWithdrawalAmount = 100;
            decimal accountBalance = retrievedAccount.Balance;

            HttpContext.Session.SetInt32("WithdrawalAmount", intendedWithdrawalAmount);

            var enteredPin = HttpContext.Session.GetString("EnteredPinNo");
            var storedPin = HttpContext.Session.GetString("PinNo");

            if (enteredPin.Length == 6)
            {
                if (enteredPin == storedPin)
                {
                    int timesWithdrawn = retrievedAccount.TimesWithdrawn;
                    int newTimesWithdrawn = retrievedAccount.TimesWithdrawn += 1;
                    decimal newAvgWithdrawal = (retrievedAccount.AvgWithdrawal * timesWithdrawn + intendedWithdrawalAmount) / newTimesWithdrawn;
                    decimal finalBalance = retrievedAccount.Balance - intendedWithdrawalAmount;

                    if (ModelState.IsValid)
                    {
                        accountContext.WithdrawalUpdateAccountDetails(accountNo, finalBalance, newAvgWithdrawal, newTimesWithdrawn);
                        withdrawalContext.InsertWithdrawalRecord(accountNo, intendedWithdrawalAmount, false);
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

        public ActionResult CheckPIN200()
        {
            HttpContext.Session.SetString("TransactionType", "Withdrawal");

            string accountNo = HttpContext.Session.GetString("AccountNo");
            Accounts retrievedAccount = accountContext.GetAccount(accountNo);

            int intendedWithdrawalAmount = 200;
            decimal accountBalance = retrievedAccount.Balance;

            HttpContext.Session.SetInt32("WithdrawalAmount", intendedWithdrawalAmount);

            var enteredPin = HttpContext.Session.GetString("EnteredPinNo");
            var storedPin = HttpContext.Session.GetString("PinNo");

            if (enteredPin.Length == 6)
            {
                if (enteredPin == storedPin)
                {
                    int timesWithdrawn = retrievedAccount.TimesWithdrawn;
                    int newTimesWithdrawn = retrievedAccount.TimesWithdrawn += 1;
                    decimal newAvgWithdrawal = (retrievedAccount.AvgWithdrawal * timesWithdrawn + intendedWithdrawalAmount) / newTimesWithdrawn;
                    decimal finalBalance = retrievedAccount.Balance - intendedWithdrawalAmount;

                    if (ModelState.IsValid)
                    {
                        accountContext.WithdrawalUpdateAccountDetails(accountNo, finalBalance, newAvgWithdrawal, newTimesWithdrawn);
                        withdrawalContext.InsertWithdrawalRecord(accountNo, intendedWithdrawalAmount, false);
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


        //public ActionResult WithdrawAmt(int? amount)
        //{
        //    Console.WriteLine(amount);
        //    return null;
        //}
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
        //}
    }
}
