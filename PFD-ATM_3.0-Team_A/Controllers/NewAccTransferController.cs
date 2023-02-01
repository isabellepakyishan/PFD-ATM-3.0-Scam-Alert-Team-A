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
    public class NewAccTransferController : Controller
    {
        private TransferDAL TransferContext = new TransferDAL();
        public IActionResult Index()
        {
            return View();
        }
        
        public ActionResult Confirm()
        {
            string accountNo = HttpContext.Session.GetString("AccountNo");
            string transferaccountNo = HttpContext.Session.GetString("TransferAccountNo");
            int transferAmount = (int) HttpContext.Session.GetInt32("TransferAmount");
            int transferPending = 0;

            TransferContext.transfer_record_save(accountNo, transferaccountNo, transferAmount, transferPending);

            return RedirectToAction("Index", "ConfirmTransfer");
        }

        public ActionResult Home()
        {
            return RedirectToAction("Index", "MainMenu");
        }
    }
}
