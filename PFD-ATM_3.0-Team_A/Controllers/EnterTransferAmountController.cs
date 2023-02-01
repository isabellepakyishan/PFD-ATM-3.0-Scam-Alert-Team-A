using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using PFD_ATM_3._0_Team_A.DAL;
using PFD_ATM_3._0_Team_A.Models;

namespace PFD_ATM_3._0_Team_A.Controllers
{
    public class EnterTransferAmountController : Controller
    {
        private TransferDAL TransferContext = new TransferDAL();
        public IActionResult Index()
        {

            return View();
        }

        public ActionResult StoreTransferAmount(IFormCollection form)
        {

            int transferAmount = Convert.ToInt32(form["transferAmount"]);
            HttpContext.Session.SetInt32("TransferAmount", transferAmount);

            string accountNo = HttpContext.Session.GetString("AccountNo");
            string transferaccountNo = HttpContext.Session.GetString("TransferAccountNo");
            bool recoerdExist = TransferContext.IsRecordExist(accountNo, transferaccountNo);

            if (recoerdExist == true)
            {
                return RedirectToAction("Index", "ConfirmTransfer");
            }
            else

                return RedirectToAction("Index", "NewAccTransfer");
        }
    }
}
