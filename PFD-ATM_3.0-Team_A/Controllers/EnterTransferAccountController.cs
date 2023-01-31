using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PFD_ATM_3._0_Team_A.DAL;
using PFD_ATM_3._0_Team_A.Models;

namespace PFD_ATM_3._0_Team_A.Controllers
{
    public class EnterTransferAccountController : Controller
    {
        private AccountsDAL accountContext = new AccountsDAL();
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult StoreTransferAccountNo(IFormCollection form)
        {
            string transferAccountNo = form["transferAccountNo"];
            bool accountExists = accountContext.IsAccountExist(transferAccountNo);

            if (transferAccountNo == HttpContext.Session.GetString("AccountNo"))
            {
                return RedirectToAction("Index", "EnterTransferAccount");
            }
            else if (accountExists)
            {
                Accounts transferAccount = accountContext.GetAccount(transferAccountNo);
                HttpContext.Session.SetString("TransferAccountNo", transferAccountNo);
                HttpContext.Session.SetString("TransferAccountName", transferAccount.Name);
                return RedirectToAction("Index", "EnterTransferAmount");
            }
            else

                return RedirectToAction("Index", "EnterTransferAccount");
        }
    }
}
