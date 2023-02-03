using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace PFD_ATM_3._0_Team_A.Controllers
{
    public class EnterPinNoController : Controller
    {
        public IActionResult Index()
        {
            return View();  
        }

        public ActionResult ReturnToHome()
        {
            return RedirectToAction("Index", "Home");
        }

        public ActionResult SimulateClear()
        {
            return RedirectToAction("Index", "EnterPinNo");
        }

        [HttpPost]
        public ActionResult StorePinNo(IFormCollection form)
        {
            string pin1 = form["pin1"];
            string pin2 = form["pin2"];
            string pin3 = form["pin3"];
            string pin4 = form["pin4"];
            string pin5 = form["pin5"];
            string pin6 = form["pin6"];
            string pin7 = form["pin7"];

            string pinNo = pin1 + pin2 + pin3 + pin4 + pin5 + pin6 + pin7;
            HttpContext.Session.SetString("EnteredPinNo", pinNo);
            HttpContext.Session.SetInt32("PinEntryCount", 1);

            return RedirectToAction("Index", "MainMenu");
        }
    }
}
