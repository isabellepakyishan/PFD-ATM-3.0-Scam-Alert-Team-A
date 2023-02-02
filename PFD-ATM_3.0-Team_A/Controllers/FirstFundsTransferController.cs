using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PFD_ATM_3._0_Team_A.DAL;
using PFD_ATM_3._0_Team_A.Models;

namespace PFD_ATM_3._0_Team_A.Controllers
{
    public class FirstFundsTransferController : Controller
    {
        private AccountsDAL accountContext = new AccountsDAL();
        private TransferDAL transferContext = new TransferDAL();
        
        public IActionResult Index()
        {
            return View();
        }
        
        public ActionResult ContinueToEnterTransferAmount()
        {
            TempData["TransferAccountName"] = HttpContext.Session.GetString("TransferAccountName");
            return RedirectToAction("Index", "EnterTransferAmount");
        }

        public ActionResult ReturnToEnterTransferAccount()
        {
            return RedirectToAction("Index", "EnterTransferAccount");
        }
    }
}
