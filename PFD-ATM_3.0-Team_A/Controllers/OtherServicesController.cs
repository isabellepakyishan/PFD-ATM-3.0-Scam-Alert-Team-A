using Microsoft.AspNetCore.Mvc;

namespace PFD_ATM_3._0_Team_A.Controllers
{
    public class OtherServicesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //public ActionResult EnterTransferAccount()
        //{
        //    return RedirectToAction("Index", "EnterTransferAccount");
        //}
    }
}
