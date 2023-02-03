using Microsoft.AspNetCore.Mvc;

namespace PFD_ATM_3._0_Team_A.Controllers
{
    public class SuccessfulTransferController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult ReturnToHome()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
