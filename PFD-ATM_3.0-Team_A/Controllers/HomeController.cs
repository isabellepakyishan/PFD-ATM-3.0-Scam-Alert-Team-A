using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using PFD_ATM_3._0_Team_A.Models;
using System.Diagnostics;
using PFD_ATM_3._0_Team_A.DAL;

namespace PFD_ATM_3._0_Team_A.Controllers
{
    public class HomeController : Controller
    {
        private AccountsDAL accountContext = new AccountsDAL();
        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public ActionResult StoreAccountNo(IFormCollection form)
        {
            string accountNo = form["accountNo"];
            bool accountExists = accountContext.IsAccountExist(accountNo);

            if (accountExists)
            {
                Accounts retrievedAccount = accountContext.GetAccount(accountNo);
                HttpContext.Session.SetString("AccountNo", accountNo);
                HttpContext.Session.SetString("PinNo", retrievedAccount.Pin);
                return RedirectToAction("Index", "EnterPinNo");
            }
            else

                return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult AjaxCallAccountNo(string sessionName)
        {
            return Json(HttpContext.Session.GetString(sessionName));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
