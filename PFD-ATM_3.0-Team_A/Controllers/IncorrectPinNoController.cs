﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PFD_ATM_3._0_Team_A.DAL;
using PFD_ATM_3._0_Team_A.Models;
using System;

namespace PFD_ATM_3._0_Team_A.Controllers
{
    public class IncorrectPinNoController : Controller
    {
        private AccountsDAL accountContext = new AccountsDAL();
        private TransferRecordsDAL transferContext = new TransferRecordsDAL();
        private WithdrawalRecordsDAL withdrawalContext = new WithdrawalRecordsDAL();

        public IActionResult Index()
        {
            //int? counter = HttpContext.Session.GetInt32("PinEntryCount");

            //if (counter == null)
            //    HttpContext.Session.SetInt32("PinEntryCount", 1);
            //else HttpContext.Session.SetInt32("PinEntryCount", (int)(counter + 1));

            return View();
        }

        public ActionResult ReturnToHome()
        {
            return RedirectToAction("Index", "Home");
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
            string transactionType = HttpContext.Session.GetString("TransactionType");

            if (reenteredPin.Length == 7)
            {
                char lastPin = reenteredPin[reenteredPin.Length - 1];
                string first6Pin = reenteredPin.Remove(6);

                if (lastPin == '*' && first6Pin == storedPin)
                {
                    HttpContext.Session.SetString("EnteredPinNo", reenteredPin);
                    return RedirectToAction("Index", "AuthenticationError");
                }
                else
                {
                    return RedirectToAction("Index", "IncorrectPinNo");
                }
            }
            else
            {
                if (reenteredPin == storedPin)
                {
                    string accountNo = HttpContext.Session.GetString("AccountNo");
                    string transferAccountNo = HttpContext.Session.GetString("TransferAccountNo");
                    Accounts retrievedAccount = accountContext.GetAccount(accountNo);

                    if (transactionType == "Withdrawal")
                    {
                        int intendedWithdrawalAmount = (int)HttpContext.Session.GetInt32("WithdrawalAmount");
                        int timesWithdrawn = retrievedAccount.TimesWithdrawn;
                        int newTimesWithdrawn = retrievedAccount.TimesWithdrawn += 1;
                        decimal avgWithdrawal = retrievedAccount.AvgWithdrawal;
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
                    else if (transactionType == "Transfer")
                    {
                        Accounts retrievedTransferAccount = accountContext.GetAccount(transferAccountNo);

                        decimal intendedTransferAmount = Convert.ToDecimal(HttpContext.Session.GetString("TransferAmount"));
                        decimal finalBalance = retrievedAccount.Balance - intendedTransferAmount;
                        decimal newBalance = retrievedTransferAccount.Balance + intendedTransferAmount;
                        DateTime transferDate = DateTime.Now;

                        if (ModelState.IsValid)
                        {
                            accountContext.TransferUpdateAccountBalance(accountNo, finalBalance, transferAccountNo, newBalance);
                            transferContext.InsertTransferRecord(transferDate, accountNo, transferAccountNo, intendedTransferAmount, false);
                        }
                        return RedirectToAction("Index", "SuccessfulTransfer");
                    }
                    else if (transactionType == "BalanceEnquiry")
                    {
                        return RedirectToAction("Index", "BalanceEnquiry");
                    }
                }
                else
                {
                    return RedirectToAction("Index", "IncorrectPinNo");
                }
            }
            return View();
        }
    }
}