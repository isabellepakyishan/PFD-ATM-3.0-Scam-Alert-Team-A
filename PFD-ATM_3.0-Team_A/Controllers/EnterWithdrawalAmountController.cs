using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PFD_ATM_3._0_Team_A.DAL;
using PFD_ATM_3._0_Team_A.Models;
using System;

namespace PFD_ATM_3._0_Team_A.Controllers
{
    public class EnterWithdrawalAmountController : Controller
    {
        private AccountsDAL accountContext = new AccountsDAL();
        private WithdrawalRecordsDAL withdrawalContext = new WithdrawalRecordsDAL();

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult ReturnToMainMenu()
        {
            return RedirectToAction("Index", "MainMenu");
        }

        public ActionResult CheckWithdrawalAmountAndPin(IFormCollection form)
        {
            HttpContext.Session.SetString("TransactionType", "Withdrawal");

            string accountNo = HttpContext.Session.GetString("AccountNo");
            Accounts retrievedAccount = accountContext.GetAccount(accountNo);

            int intendedWithdrawalAmount = Convert.ToInt32(form["withdrawalAmount"]);
            int accountWithdrawalLimit = Convert.ToInt32(retrievedAccount.WithdrawalLimit);
            decimal accountBalance = retrievedAccount.Balance;
            decimal avgWithdrawal = retrievedAccount.AvgWithdrawal;

            int checkWithdrawalMultiples = intendedWithdrawalAmount % 10;

            if (checkWithdrawalMultiples != 0)
            {
                TempData["Message"] = "Invalid amount entered. Please enter a valid withdrawal amount.";
                return RedirectToAction("Index", "EnterWithdrawalAmount");
            }
            else if (intendedWithdrawalAmount == 0)
            {
                TempData["Message"] = "You cannot withdraw $0🤯 Please enter a valid withdrawal amount.";
                return RedirectToAction("Index", "EnterWithdrawalAmount");
            }
            else if (intendedWithdrawalAmount < 0)
            {
                TempData["Message"] = "You cannot withdraw a negative amount🤯 Please enter a valid withdrawal amount.";
                return RedirectToAction("Index", "EnterWithdrawalAmount");
            }
            else
            {
                if (intendedWithdrawalAmount > accountBalance)
                {
                    TempData["Message"] = "Withdrawal amount entered exceeds your account balance. Please enter a valid withdrawal amount.";
                    return RedirectToAction("Index", "EnterWithdrawalAmount");
                }
                else if (intendedWithdrawalAmount > accountWithdrawalLimit)
                {
                    TempData["Message"] = "Withdrawal amount entered exceeds daily withdrawal limit. Please enter a valid withdrawal amount.";
                    return RedirectToAction("Index", "EnterWithdrawalAmount");
                }
                else if (intendedWithdrawalAmount > avgWithdrawal)
                {
                    HttpContext.Session.SetInt32("WithdrawalAmount", intendedWithdrawalAmount);

                    decimal avgWithdrawal2 = Math.Round(avgWithdrawal, 2);
                    HttpContext.Session.SetString("AvgWithdrawalAmount", Convert.ToString(avgWithdrawal2));

                    return RedirectToAction("Index", "ExceedAvgWithdrawal");
                }
                else
                {
                    HttpContext.Session.SetInt32("WithdrawalAmount", intendedWithdrawalAmount);

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
                            decimal finalBalance = accountBalance - intendedWithdrawalAmount;
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
        //}
    }
}