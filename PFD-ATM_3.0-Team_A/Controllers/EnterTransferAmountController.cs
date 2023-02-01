using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace PFD_ATM_3._0_Team_A.Controllers
{
    public class EnterTransferAmountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult StoreTransferAmount(IFormCollection form)
        {
            int transferAmount = Convert.ToInt32(form["transferAmount"]);
            HttpContext.Session.SetInt32("TransferAmount", transferAmount);
            return RedirectToAction("Index", "ConfirmTransfer");
        }
    }
}
