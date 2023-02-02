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
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult CheckWithdrawalAmountAndPin(IFormCollection form)
        {
            string accountNo = HttpContext.Session.GetString("AccountNo");
            Accounts retrievedAccount = accountContext.GetAccount(accountNo);

            int intendedWithdrawalAmount = Convert.ToInt32(form["withdrawalAmount"]);
            int accountWithdrawalLimit = Convert.ToInt32(retrievedAccount.WithdrawalLimit);
            decimal accountBalance = retrievedAccount.Balance;

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
                else
                {
                    HttpContext.Session.SetInt32("WithdrawalAmount", intendedWithdrawalAmount);
                    
                    var enteredPin = HttpContext.Session.GetString("EnteredPinNo");
                    var storedPin = HttpContext.Session.GetString("PinNo");

                    if (enteredPin.Length == 6)
                    {
                        if (enteredPin == storedPin)
                        {
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
