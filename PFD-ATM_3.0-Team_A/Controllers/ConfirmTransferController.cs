using Microsoft.AspNetCore.Mvc;

namespace PFD_ATM_3._0_Team_A.Controllers
{
    public class ConfirmTransferController : Controller
    {
        public IActionResult Index()
        {
            //Session["AccountNo"]
            return View();
        }
    }
}
